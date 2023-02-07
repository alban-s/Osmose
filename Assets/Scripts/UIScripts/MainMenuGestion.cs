using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class MainMenuGestion : MonoBehaviour
{
    public GameObject mainWindow;
    public GameObject settingWindow;
    public GameObject newGameWindow;
    public GameObject joinGameWindow;
    public GameObject initPersoWindow;
    public GameObject cinematicWindow;
    private double timeBegin;
    private GameObject NetworkManager;


    // Start is called before the first frame update
    void Start()
    {
        NetworkManager = GameObject.Find("NetworkManager");
        NetworkManager.SetActive(true);
        Screen.fullScreen = false;
        timeBegin = Time.time;

        mainWindow.SetActive(true);
        settingWindow.SetActive(false);
        newGameWindow.SetActive(false);
        joinGameWindow.SetActive(false);
        initPersoWindow.SetActive(false);
        cinematicWindow.SetActive(false);
}

    void Update()
    {
        /*if(Time.time - timeBegin >= 32.5)
        {
            cinematicWindow.SetActive(false);
            mainWindow.SetActive(true);
            netHUD.SetActive(true);
        }*/
    }

    public void StartNewGame()
    {
        newGameWindow.SetActive(true);
    }

    public void JoinGame()
    {
        joinGameWindow.SetActive(true);
    }

    public void Settings()
    {
        settingWindow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
