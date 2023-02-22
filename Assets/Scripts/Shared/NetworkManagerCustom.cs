using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Osmose.Gameplay;

public class NetworkManagerCustom : NetworkManager
{

    TeamColour team = TeamColour.Blue;
    private int index_pos_red = 0;
    private int index_pos_blue = 0;
    public GameObject[] red_spawns;
    public GameObject[] blue_spawns;
    private GameObject gameManager;

    public override void Awake()
    {
        base.Awake();
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

        NetworkServer.Spawn(gameManager);
    }
    void OnCreatePlayer(NetworkConnectionToClient conn, CreatePlayerMessage message)
    {
        this.team = message.team;
        Transform pos = GetStartPosition();
        Debug.Log("team ll : "+this.team);
        Debug.Log("pos ll : "+pos);

        gameManager = GameObject.Find("GameManager");
        int remaining = gameManager.GetComponent<GameManager>().GetMaxNbOfPlayer() - gameManager.GetComponent<GameManager>().GetPlayersReadyCount();

        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject player = pos != null?Instantiate(playerPrefab,pos.position, pos.rotation)
                                        :Instantiate(playerPrefab);
        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        player.name = message.name;

        // call this to use this gameobject as the primary controller
        if (remaining > 0 && !gameManager.GetComponent<Timer>().GameOn())
            NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override Transform GetStartPosition()
        {
            // first remove any dead transforms
            if (team == TeamColour.Blue){
                index_pos_blue =( index_pos_blue + 1) % blue_spawns.Length;
                return blue_spawns[index_pos_blue].transform;
            }
            if (team == TeamColour.Red){
                index_pos_red =( index_pos_red + 1) % red_spawns.Length;
                return red_spawns[index_pos_red].transform;
            }
            return blue_spawns[1].transform;
        }


}
