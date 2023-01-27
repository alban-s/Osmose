using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public static void RegisterPlayer(string netID, GameObject player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static GameObject GetPlayer(string playerId)
    {
        return players[playerId];
    }

/*    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach(string playerId in players.Keys)
        {
            GUILayout.Label(playerId + " - " + players[playerId].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
}
