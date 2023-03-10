using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
using Osmose.Gameplay;

public class InitPerso : NetworkBehaviour
{
    IEnumerator wait_and_close()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject localPlayer = NetworkClient.localPlayer.gameObject;
        localPlayer.GetComponent<PlayerController>().enabled = false;
        localPlayer.GetComponent<PlayerMotor>().enabled = false;
        thisWindow.SetActive(false);
    }
    public GameObject mainWindow;
    public GameObject thisWindow;
    public Scene GameScene;
    public TMP_Dropdown equipeDropdown;
    public TextMeshProUGUI pseudo;

    public int choseTeam = 0;
    public string chosePseudo;
    List<string> options;

    GameObject manager;


    // Start is called before the first frame update
    void Start()
    {
        options = new List<string>();
        options.Add("Rouge");
        options.Add("Bleu");

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
        NetworkManagerCustom nmc = GameObject.Find("NetworkManager").GetComponent<NetworkManagerCustom>();
        
        NetworkManagerCustom.CreatePlayerMessage createdMessage = new NetworkManagerCustom.CreatePlayerMessage
        {
            team = (TeamColour)choseTeam,
            name = chosePseudo
        };

        NetworkClient.Send(createdMessage);
        // NetworkClient.AddPlayer();

        mainWindow.SetActive(false);
        //SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        
        StartCoroutine(wait_and_close());
    }
}

