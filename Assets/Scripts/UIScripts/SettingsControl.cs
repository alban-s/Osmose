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
    public GameObject data;


    Resolution[] resolutions;

    // Start is called before the first frame update
    public void Start()
    {
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

    public void AmbiantMusic(float volume)
    {
        AmbiantMixer.SetFloat("ambiantMusic", volume);
    }
  
    public void EffectMusic(float effetsSonore)
    {
        EffectMixer.SetFloat("effectMusic", effetsSonore);
    }

    public void SetCamSentibilityX(float sentibility)
    {
        data.GetComponent<CameraData>().setCamX(sentibility);
    }

    public void SetCamSentibilityY(float sentibility)
    {
        data.GetComponent<CameraData>().setCamY(sentibility);
    }

    public void ReturnMenu()
    {
        thisWindow.SetActive(false);
    }
}

