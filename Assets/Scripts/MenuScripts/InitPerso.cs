using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;

public class InitPerso : MonoBehaviour
{
    public GameObject thisWindow;
    public Scene GameScene;
    public TMP_Dropdown equipeDropdown;
    public TextMeshProUGUI pseudo;

    private int choseTeam = 0;
    private string chosePseudo;
    List<string> options;

    // Start is called before the first frame update
    void Start()
    {
        options = new List<string>();
        options.Add("Bleu");
        options.Add("Rouge");

        equipeDropdown.ClearOptions();
        equipeDropdown.AddOptions(options);

        chosePseudo = "pseudo";
    }

    public void ReturnMenu()
    {
        print("lancer scene de jeu");
        thisWindow.SetActive(false);
    }

    public void LaunchGame()
    {
        print("lancer scene de jeu avec perso pseudo" + chosePseudo + "et equipe " + options[choseTeam]);
        NetworkClient.AddPlayer();
        thisWindow.SetActive(false);
    }

    public void SetTeam(int teamIdx)
    {
        choseTeam = teamIdx;
        print("set team is " + teamIdx.ToString());
/*        equipeDropdown.value = teamIdx;
        equipeDropdown.RefreshShownValue();*/
    }

    public void SetPerso(string pseudo)
    {
        chosePseudo = pseudo;
    }
}

