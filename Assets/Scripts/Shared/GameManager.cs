using Osmose.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
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

    public void update_names(){
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        foreach (GameObject player in players)
        {
            gameObject.GetComponent<PlayerSetup>().update_player_name();
        }

    }
    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string playerId in players.Keys)
        {
            GUILayout.Label(playerId + " - " + players[playerId].GetComponent<Team>().team);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
}
