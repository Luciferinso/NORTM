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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mostly finding all the stuff that I've defined before.
        level = GameObject.Find("CurrentLevel");
        playerRigid = GetComponent<Rigidbody>();
        player = gameObject;
        scapegoat = GameObject.Find("Scapegoat");
        focalPoint = GameObject.Find("FocalPoint");
        playerRigid.AddForce(Vector3.up, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //vestigial, only here for rollback purposes
        //playerRigid.AddForce(transform.)
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
        //rotate WS
        scapegoat.transform.RotateAround(player.transform.position, focalPoint.transform.forward, horizontalInput * rotationMultiplier * Time.deltaTime * -1);
        //rotate AD
        scapegoat.transform.RotateAround(player.transform.position, focalPoint.transform.right, verticalInput * rotationMultiplier * Time.deltaTime * 1);
    }
}








//they'll never know
//sets the level up to be able to rotate about the player
//level.transform.position = player.transform.position;
//scapegoat.transform.position = Vector3.zero;
// level.transform.rotation = Quaternion.Euler(0f,0f,horizontalInput * rotationMultiplier * Time.deltaTime);