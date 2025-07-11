using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    public GameObject[] levels;
    public PlayerControl playerControl;
    public TextMeshProUGUI scoreText;
    public Button restartText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("PlayerPlaceholder").GetComponent<PlayerControl>();
        Debug.Log(playerControl.crntLvl);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerControl.isGameOver == true)
        {
            restartText.gameObject.SetActive(true);
        }
    }
}
