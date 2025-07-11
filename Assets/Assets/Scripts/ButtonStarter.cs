using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonStarter : MonoBehaviour
{
    public Button button;  //Both Button and TItle objects 
    public GameObject titleScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();  //waits for button to be pressed and set the button component 
        button.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame() //This turns off the title after "Start Game?" is pressed. 
    {
        titleScreen.gameObject.SetActive(false);
    }
}
