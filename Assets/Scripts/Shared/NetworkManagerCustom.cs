using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerCustom : NetworkManager
{
    public GameObject gameManager;
    // public override void OnClientConnect()
    // {
    //     base.OnClientConnect();
    //     gameManager.GetComponent<GameManager>().update_names();
    //     List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    //     Debug.Log("size "+players.Count);
    //     foreach (GameObject player in players)
    //     {
    //         string name = player.transform.name;
    //         player.GetComponent<PlayerSetup>().gather_names();
    //         Debug.Log(" name player : " + player.transform.name);
    //     }
        
    // }
}
