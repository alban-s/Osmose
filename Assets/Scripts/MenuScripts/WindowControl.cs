using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindowControl : MonoBehaviour
{
    public GameObject thisWindow;
    public GameObject settingWindow;
    public GameObject endGameWindow;
 

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        endGameWindow.SetActive(false);
        settingWindow.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  SettingButton();

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
