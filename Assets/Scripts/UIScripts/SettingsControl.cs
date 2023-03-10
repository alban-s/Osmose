using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using TMPro;

public class SettingsControl : MonoBehaviour
{
    public GameObject thisWindow;
    public AudioMixer AmbiantMixer;
    public AudioMixer EffectMixer;
    public TMP_Dropdown resolutionDropdown;
    private GameObject manager;


    Resolution[] resolutions;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("GameManager");
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        int curResolutionIdx = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                curResolutionIdx = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = curResolutionIdx;
        //resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolistionIndex)
    {
        Resolution resolution = resolutions[resolistionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        AmbiantMixer.SetFloat("ambiantMusic", volume);
    }

    public void SetSoundEffect(float soundEffect)
    {
        EffectMixer.SetFloat("effectMusic", soundEffect);
    }

    public void SetCamSentibilityX(float sentibility)
    {
        manager.GetComponent<CameraData>().setCamX(sentibility);
    }

    public void SetCamSentibilityY(float sentibility)
    {
        manager.GetComponent<CameraData>().setCamY(sentibility);
    }

    public void ReturnMenu()
    {
        thisWindow.SetActive(false);
    }
}

