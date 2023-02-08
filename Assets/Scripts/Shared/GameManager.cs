using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Osmose.Gameplay;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private ushort nbOfPlayer;

    public ushort GetNbOfPlayer()
    {
        return nbOfPlayer;
    }

    public void SetNbOfPlayer(ushort value)
    {
        nbOfPlayer = value;
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
}
