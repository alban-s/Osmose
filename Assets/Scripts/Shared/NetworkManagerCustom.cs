using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Osmose.Gameplay;

public class NetworkManagerCustom : NetworkManager
{

    TeamColour team = TeamColour.Blue;

    private int nbPlayersConnected = 0;
    private int index_pos_red = 0;
    private int index_pos_blue = 0;
    public GameObject[] red_spawns;
    public GameObject[] blue_spawns;
    private GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public struct CreatePlayerMessage : NetworkMessage
    {
        public TeamColour team;
        public string name;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }
    void OnCreatePlayer(NetworkConnectionToClient conn, CreatePlayerMessage message)
    {
        this.team = message.team;
        Transform pos = GetStartPosition();
        Debug.Log("team ll : "+this.team);
        Debug.Log("pos ll : "+pos);
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject player = pos != null?Instantiate(playerPrefab,pos.position, pos.rotation)
                                        :Instantiate(playerPrefab);
        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        player.name = message.name;

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, player);

        nbPlayersConnected++;
        Debug.Log("nombre attendu : " + gameManager.GetComponent<GameManager>().GetNbOfPlayer() + "  nombre actuel : " + nbPlayersConnected);
        if (nbPlayersConnected == gameManager.GetComponent<GameManager>().GetNbOfPlayer())
        {
            gameManager.GetComponent<Timer>().ResetTimer();
            Debug.Log("TIMER GO");
        }  
    }

    public override Transform GetStartPosition()
        {
            // first remove any dead transforms
            if (team == TeamColour.Blue){
                index_pos_blue =( index_pos_blue + 1) % blue_spawns.Length;
                return blue_spawns[index_pos_blue].transform;
                Debug.Log("BLUE : ");
            }
            if (team == TeamColour.Red){
                index_pos_red =( index_pos_red + 1) % red_spawns.Length;
                return red_spawns[index_pos_red].transform;
                Debug.Log("RED : ");
            }
            return blue_spawns[1].transform;
        }


}
