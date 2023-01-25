using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGestion : MonoBehaviour
{
    public GameObject mainWindow;
    public GameObject settingWindow;
    public GameObject newGameWindow;
    public GameObject joinGameWindow;
    public GameObject initPersoWindow;
    private double timeBegin;


    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
        timeBegin = Time.time;

        mainWindow.SetActive(true);
        settingWindow.SetActive(false);
        newGameWindow.SetActive(false);
        joinGameWindow.SetActive(false);
        initPersoWindow.SetActive(false);


}

    void Update()
    {
        /*if(Time.time - timeBegin > 2)
        {
            mainWindow.SetActive(true);
        }*/
    }

    public void StartNewGame()
    {
        newGameWindow.SetActive(true);
        mainWindow.SetActive(false);
    }

    public void JoinGame()
    {
        joinGameWindow.SetActive(true);
        mainWindow.SetActive(false);
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
