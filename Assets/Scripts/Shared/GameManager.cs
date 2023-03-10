using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Osmose.Gameplay;

public class GameManager : NetworkBehaviour
{
    [SyncVar]
    public bool gameStarted = false;

    [SyncVar]
    public int maxNbOfPlayer;


    [Command(requiresAuthority = false)]
    public void StartGame(){
        this.gameStarted = true;
    }
    [Command(requiresAuthority = false)]
    public void StopGame(){
        this.gameStarted = true;
    }
    public int GetMaxNbOfPlayer()
    {
        return maxNbOfPlayer;
    }

    public void SetMaxNbOfPlayer(int value)
    {
        maxNbOfPlayer = value;
    }

    public List<GameObject> GetPlayerBlueList()
    {
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        List<GameObject> bluePlayer = new List<GameObject>();
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Team>().GetTeam() == TeamColour.Blue)
                bluePlayer.Add(player);
        }
        return bluePlayer;
    }

    public List<GameObject> GetPlayerRedList()
    {
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        List<GameObject> redPlayer = new List<GameObject>();
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Team>().GetTeam() == TeamColour.Red)
                redPlayer.Add(player);
        }
        return redPlayer;
    }

    public int GetBlueCount(){
        return GetPlayerBlueList().Count;
    }

    public int GetRedCount(){
        return GetPlayerRedList().Count;
    }

    public bool AllPlayersReady(){
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        foreach (GameObject player in players)
        {
            if(!player.GetComponent<PlayerSetup>().ready) return false;
        }
        return true;
    }

    public int GetPlayersReadyCount(){
        int count = 0;
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        foreach (GameObject player in players)
        {
            if(player.GetComponent<PlayerSetup>().ready) count++;
        }
        return count;
    }

    public int GetBluePlayersNotReadyCount()
    {
        int count = 0;
        List<GameObject> players = GetPlayerBlueList();
        foreach (GameObject player in players)
        {
            if (!player.GetComponent<PlayerSetup>().ready) count++;
        }
        return count;
    }
    public int GetRedPlayersNotReadyCount()
    {
        int count = 0;
        List<GameObject> players = GetPlayerRedList();
        foreach (GameObject player in players)
        {
            if (!player.GetComponent<PlayerSetup>().ready) count++;
        }
        return count;
    }

    public int GetPlayersConnectedCount()
    {
        return (new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"))).Count;
    }

}
