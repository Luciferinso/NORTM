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
    public bool isInputDisabled;
    public float torqueMultiplier;
    public int crntLvl = -1;
    private LevelHandler levelHandler;
    public bool isGameOver;
    float CountDown = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ughhh last minute fix for the level system
        crntLvl = crntLvl + 1;
        //mostly finding all the stuff that I've defined before.
        level = GameObject.Find("CurrentLevel");
        playerRigid = GetComponent<Rigidbody>();
        player = gameObject;
        scapegoat = GameObject.Find("Scapegoat");
        focalPoint = GameObject.Find("FocalPoint");
        spawnPoint = GameObject.Find("SpawnPoint");
        levelHandler = GameObject.Find("LevelHandler").GetComponent<LevelHandler>();
        isGameOver = false;
        isInputDisabled = true;
    }

    public void StartGame()
    {

        scapegoat.transform.rotation = Quaternion.identity;
        playerRigid.linearVelocity = Vector3.zero;
        playerRigid.angularVelocity = Vector3.zero;
        //set player position to the spawnpoint
        playerRigid.position = spawnPoint.transform.position;

        // Reset world
        scapegoat.transform.rotation = Quaternion.identity;
        level.transform.position = player.transform.position;
        scapegoat.transform.position = Vector3.zero;
        isInputDisabled = false;
    }
    void GameOver()
    {
        Debug.Log("game over state reached on " + crntLvl);
        isGameOver = true;
        playerRigid.Sleep();
    }
    // Update is called once per frame
    void Update()
    {
        //vestigial, only here for rollback purposes
        //playerRigid.AddForce(transform.)
        //simple script resetting the player position
        if (player.transform.position.y < lowerBound)
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (crntLvl == 3)
        {
            CountDown = CountDown - Time.deltaTime;
            if (CountDown < 0)
            {
                GameOver();
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        isOnGround = true;
        if (other.gameObject.CompareTag("FinishLine") || Input.GetKeyDown(KeyCode.C))
        {
            // Disables previous level (level just completed)
            Destroy(other.gameObject);
            levelHandler.levels[crntLvl].SetActive(false);
            crntLvl = crntLvl + 1;
            playerRigid.linearVelocity = Vector3.zero;
            isInputDisabled = true;
            // Activates next level
            Invoke("StartGame", 0.1f);
            levelHandler.levels[crntLvl].SetActive(true);
            Debug.Log("level increased to " + crntLvl);
        }
    }
    void LateUpdate()
    {
        if (isInputDisabled == false)
        {
            //mostly for camera handling
            focalPoint.transform.position = player.transform.position;
        }
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
        if (isInputDisabled == false)
        {
            //rotate AD
            scapegoat.transform.RotateAround(player.transform.position, focalPoint.transform.forward, horizontalInput * rotationMultiplier * Time.deltaTime * -1);
            playerRigid.AddTorque(focalPoint.transform.forward * horizontalInput * Time.deltaTime * torqueMultiplier * -1);
            //rotate WS
            scapegoat.transform.RotateAround(player.transform.position, focalPoint.transform.right, verticalInput * rotationMultiplier * Time.deltaTime * 1);
            playerRigid.AddTorque(focalPoint.transform.right * verticalInput * Time.deltaTime * torqueMultiplier);
        }
    }
}








//they'll never know
//sets the level up to be able to rotate about the player
//level.transform.position = player.transform.position;
//scapegoat.transform.position = Vector3.zero;
// level.transform.rotation = Quaternion.Euler(0f,0f,horizontalInput * rotationMultiplier * Time.deltaTime);