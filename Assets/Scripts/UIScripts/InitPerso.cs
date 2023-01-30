using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;

public class InitPerso : MonoBehaviour
{
    IEnumerator wait_and_close()
    {
        yield return new WaitForSeconds(0.5f);
        thisWindow.SetActive(false);
    }
    public GameObject thisWindow;
    public Scene GameScene;
    public TMP_Dropdown equipeDropdown;
    public TextMeshProUGUI pseudo;

    public int choseTeam = 0;
    public string chosePseudo;
    List<string> options;

    // Start is called before the first frame update
    void Start()
    {
        options = new List<string>();
        options.Add("Rouge");
        options.Add("Jaune");

        equipeDropdown.ClearOptions();
        equipeDropdown.AddOptions(options);

        chosePseudo = pseudo.text;
    }


    public void ReturnMenu()
    {
        print("lancer scene de jeu");
        thisWindow.SetActive(false);
    }

    public void LaunchGame()
    {
        choseTeam = equipeDropdown.value;
        chosePseudo  = pseudo.text;
        print("lancer scene de jeu avec perso pseudo" + chosePseudo + "et equipe " + options[choseTeam]);
        NetworkClient.AddPlayer();

        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        StartCoroutine(wait_and_close());
    }
}

