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

    public void SettingButton()
    {
        if (settingWindow.activeSelf)
        {
            settingWindow.SetActive(false);
        }
        else
        {
            settingWindow.SetActive(true);
        }
    }
}
