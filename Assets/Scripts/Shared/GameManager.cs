using Osmose.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public static void RegisterPlayer(string netID, GameObject player)
    {
        //string playerId = playerIdPrefix + netID;
        players.Add(netID, player);
        player.transform.name = netID;
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static GameObject GetPlayer(string playerId)
    {
        return players[playerId];
    }

    public static void SetPlayer(GameObject player)
    {
        players[player.transform.name] = player;
    }

    public static Dictionary<string, GameObject> GetPlayerList()
    {
        return players;
    }

    public List<GameObject> GetPlayerBlueList()
    {
        List<GameObject> bluePlayer = new List<GameObject>();
        foreach (KeyValuePair<string, GameObject> player in players)
        {
            if (player.Value.GetComponent<Team>().GetTeam() == TeamColour.Blue)
                bluePlayer.Add(player.Value);
        }
        return bluePlayer;
    }

    public List<GameObject> GetPlayerRedList()
    {
        List<GameObject> redPlayer = new List<GameObject>();
        foreach (KeyValuePair<string, GameObject> player in players){
            if (player.Value.GetComponent<Team>().GetTeam() == TeamColour.Red)
                redPlayer.Add(player.Value);
        }
        return redPlayer;
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
