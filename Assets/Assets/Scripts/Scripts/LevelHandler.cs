using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelHandler : MonoBehaviour
{
    public GameObject[] levels;
    public PlayerControl playerControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("PlayerPlaceholder").GetComponent<PlayerControl>();
        Debug.Log(playerControl.crntLvl);
    }

    // Update is called once per frame
    void Update()
    {
        levels[playerControl.crntLvl].SetActive(true);
    }
}
