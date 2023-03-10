using static System.Math;
using UnityEngine;
using Mirror;
using Osmose.Gameplay;
using System.Collections.Generic;
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    GameObject initValueGUI;

    [SerializeField]
    private GameObject GameWindowPrefab;
    private GameObject GameWindowInstance;
    private GameObject gameManager;

    [SyncVar]
    public bool ready = false;
    Camera sceneCamera;
     private void Start()
    {
        initValueGUI = GameObject.Find("InitPersoPanel");
        //Debug.LogError(initValueGUI.name);
        
        if (!isLocalPlayer)
        {
            DisableComponents();
            //AssignRemoteLayer();
        }
        else
        {
            InitClientValues();

            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            GameWindowInstance = Instantiate(GameWindowPrefab);

            GameWindow ui = GameWindowInstance.transform.GetChild(0).gameObject.GetComponent<GameWindow>();
            SelectPtsWindow uiSelect = GameWindowInstance.transform.GetChild(4).gameObject.GetComponent<SelectPtsWindow>();

            if (ui == null)
            {
                Debug.LogError("Pas de gamewindow sur gamewindowinstance");
            }
            else
            {
                ui.SetPlayer(gameObject);
                uiSelect.SetPlayer(gameObject);
            }
        }
        if (!isServer)
        {
            GetComponent<NetworkTransformReliable>().syncDirection = SyncDirection.ClientToServer;
        }
        broadcast_players_infos();
    }

    [Command]
    public void CmdSetReady(bool rdy){
        ready = rdy;
    }

    [Command]
    public void SendClientValues(TeamColour teamColor, string name)
    {
        gameObject.GetComponent<Team>().team = teamColor;
        gameObject.transform.name = name;
    }
    public void InitClientValues()
    {
        if (initValueGUI != null)
        {
            InitPerso ip = initValueGUI.GetComponent<InitPerso>();
            if (ip != null)
            {
                gameManager = GameObject.Find("GameManager");
                if ((TeamColour)ip.choseTeam == TeamColour.Red)
                {
                    Debug.LogError(gameManager.GetComponent<GameManager>().GetRedCount());
                    Debug.LogError(Ceiling((double)gameManager.GetComponent<GameManager>().GetMaxNbOfPlayer() / 2.0f));
                    if (gameManager.GetComponent<GameManager>().GetRedCount() <= Ceiling((double)gameManager.GetComponent<GameManager>().GetMaxNbOfPlayer() / 2.0f))
                        gameObject.GetComponent<Team>().team = (TeamColour)ip.choseTeam;
                    else
                        gameObject.GetComponent<Team>().team  = TeamColour.Blue;
                }
                else if ((TeamColour)ip.choseTeam == TeamColour.Blue)
                {
                    if (gameManager.GetComponent<GameManager>().GetBlueCount() <= Ceiling((double)gameManager.GetComponent<GameManager>().GetMaxNbOfPlayer() / 2.0f))
                        gameObject.GetComponent<Team>().team = (TeamColour)ip.choseTeam;
                    else
                        gameObject.GetComponent<Team>().team = TeamColour.Red;
                }
                Debug.LogError(gameObject.GetComponent<Team>().team);
                gameObject.transform.name = ip.chosePseudo;
                gameObject.GetComponent<Team>().ChoixEquipe();
                SendClientValues(gameObject.GetComponent<Team>().team, ip.chosePseudo);
            }
        }
    }

    [Command]
    public void broadcast_players_infos(){
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        Debug.Log("nb player "+players.Count);
        foreach (GameObject player in players)
        {
            string name = player.transform.name;
            player.GetComponent<PlayerSetup>().update_player_name_clients(name);
            Debug.Log(" broadcast name player : " + player.transform.name);
            TeamColour team = player.GetComponent<Team>().team;
            player.GetComponent<PlayerSetup>().update_player_team_clients(team);
            Debug.Log(" broadcast team player : " + player.GetComponent<Team>().team);
        }
    }

    [ClientRpc]
    public void update_player_name_clients(string name){
        gameObject.transform.name = name;
    }

    [ClientRpc]
    public void update_player_team_clients(TeamColour team){
        gameObject.GetComponent<Team>().team = team;
        gameObject.GetComponent<Team>().ChoixEquipe();
    }

    private void DisableComponents()
    {
        // On va boucler sur les diff???rents composants renseign???s et les d???sactiver si ce joueur n'est pas le notre
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
       
    }

    private void OnDisable()
    {
        Destroy(GameWindowInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
