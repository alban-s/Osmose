using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Osmose.Game;


public class WindowControl : MonoBehaviour
{
    public GameObject choseScoreWindow;
    public GameObject gameWindow;
    public GameObject settingWindow;
    public GameObject endGameWindow;


    bool gameOn = true;


    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        choseScoreWindow.SetActive(true);
        gameWindow.SetActive(false);
        endGameWindow.SetActive(false);
        settingWindow.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SettingButton();
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

