using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowControl : MonoBehaviour
{
    public GameObject choseScoreWindow;
    public GameObject gameWindow;
    public GameObject settingWindow;
    public GameObject endGameWindow;

    private GameObject manager;

    private bool gameOn;
    private bool gameStart;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        Screen.fullScreen = true;
        choseScoreWindow.SetActive(true);
        gameWindow.SetActive(false);
        endGameWindow.SetActive(false);
        settingWindow.SetActive(false);
        gameOn = false;
        gameStart = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SettingButton();

        gameOn = manager.GetComponent<Timer>().GameOn();


        // Lancement de la fenetre de jeu quand partie lancé
        if (gameOn)
        {
            gameWindow.SetActive(true);
            gameStart = true;
        } // Lancement de la fenetre de fin de jeu quand la partie finie
        else if(!gameOn && gameStart)
        {
            gameWindow.SetActive(false);
            endGameWindow.SetActive(true);
        }
    }

    public void SettingButton()
    {
        if (settingWindow.activeSelf)
        {
            settingWindow.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            settingWindow.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

