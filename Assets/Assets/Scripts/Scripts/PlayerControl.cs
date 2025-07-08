using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    private GameObject level;
    private GameObject scapegoat;
    private Rigidbody playerRigid;
    private GameObject player;
    public float rotationMultiplier;
    public GameObject focalPoint;
    private GameObject spawnPoint;
    public float lowerBound = -15f;
    public float jumpForce = 20f;
    public bool isOnGround;
    public float torqueMultiplier;
    public int crntLvl = -1;
    private LevelHandler levelHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mostly finding all the stuff that I've defined before.
        level = GameObject.Find("CurrentLevel");
        playerRigid = GetComponent<Rigidbody>();
        player = gameObject;
        scapegoat = GameObject.Find("Scapegoat");
        focalPoint = GameObject.Find("FocalPoint");
        spawnPoint = GameObject.Find("SpawnPoint");
        levelHandler = GameObject.Find("LevelHandler").GetComponent<LevelHandler>();
        StartGame();
    }

    public void StartGame()
    {
        crntLvl += 1;

        //set player position to the spawnpoint
        player.transform.position = spawnPoint.transform.position;
        //wake the player's rigidbody up
        playerRigid.AddForce(Vector3.up, ForceMode.Impulse);
        //find an endpoint
    }

    // Update is called once per frame
    void Update()
    {
        //vestigial, only here for rollback purposes
        //playerRigid.AddForce(transform.)
        //simple script resetting the player position
        if (player.transform.position.y < lowerBound)
        {
            Debug.Log("game over state reached on " + crntLvl);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        isOnGround = true;
        if (other.gameObject.CompareTag("FinishLine"))
        {
            // Disables previous level (level just completed)
            levelHandler.levels[crntLvl].SetActive(false);
            // Activates next level
            StartGame();
            Debug.Log("level increased to " + crntLvl);
        }
    }
    void LateUpdate()
    {
        //mostly for camera handling
        focalPoint.transform.position = player.transform.position;
    }
    void FixedUpdate()
    {
        //input stuffs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        //sets the level up to be able to rotate about the player
        level.transform.position = player.transform.position;
        scapegoat.transform.position = Vector3.zero;
        //defines a 0,0,0
        scapegoat.transform.rotation = Quaternion.identity;
        //rotate about the player
        //rotate AD
        scapegoat.transform.RotateAround(player.transform.position, focalPoint.transform.forward, horizontalInput * rotationMultiplier * Time.deltaTime * -1);
        playerRigid.AddTorque(focalPoint.transform.forward * horizontalInput * Time.deltaTime * torqueMultiplier * -1);
        //rotate WS
        scapegoat.transform.RotateAround(player.transform.position, focalPoint.transform.right, verticalInput * rotationMultiplier * Time.deltaTime * 1);
        playerRigid.AddTorque(focalPoint.transform.right * verticalInput * Time.deltaTime * torqueMultiplier);
    }
}








//they'll never know
//sets the level up to be able to rotate about the player
//level.transform.position = player.transform.position;
//scapegoat.transform.position = Vector3.zero;
// level.transform.rotation = Quaternion.Euler(0f,0f,horizontalInput * rotationMultiplier * Time.deltaTime);