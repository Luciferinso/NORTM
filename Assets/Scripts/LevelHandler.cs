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
    public Button exitGame;
    public GameObject pauseMenu;
    public bool isPaused = false;
    public GameObject controls;
    public GameObject exit;
    public GameObject restart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("PlayerPlaceholder").GetComponent<PlayerControl>();
        Debug.Log(playerControl.crntLvl);
        controls.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerControl.isGameOver == true)
        {
            restartText.gameObject.SetActive(true);
        }
        if (playerControl.isInputDisabled == false)
        {
            exitGame.gameObject.SetActive(false);
        }
        if (playerControl.isInputDisabled == true || playerControl.crntLvl == 4)
        {
            exitGame.gameObject.SetActive(true);
        }
        if (playerControl.crntLvl == 4)
        {
            restartText.gameObject.SetActive(true);
        }
        if (isPaused == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Debug.Log("paused");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Debug.Log("unpaused");
        }
    }
}
