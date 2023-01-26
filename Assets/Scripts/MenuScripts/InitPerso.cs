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

    private int choseTeam = 0;
    private int chosePerso = 0;
    List<string> options;

    // Start is called before the first frame update
    void Start()
    {
        /*
        equipeDropdown.ClearOptions();
        options.Add("Bleu");
        options.Add("Rouge");

        equipeDropdown.AddOptions(options);
        */
    }

    public void ReturnMenu()
    {
        print("lancer scene de jeu");
        thisWindow.SetActive(false);
    }

    public void LaunchGame()
    {
        //print("lancer scene de jeu avec perso id" + chosePerso + "et equipe " + options[choseTeam]);
        NetworkClient.AddPlayer();
        thisWindow.SetActive(false);
    }

    public void SetTeam(int teamIdx)
    {
        choseTeam = teamIdx;
    }

    public void SetPerso(string PersoId)
    {
        chosePerso = int.Parse(PersoId);
    }
}

