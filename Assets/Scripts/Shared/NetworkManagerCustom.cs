using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerCustom : NetworkManager
{
    public GameObject gameManager;
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        gameManager.GetComponent<GameManager>().update_names();
        Debug.Log("OULOULOULOULOULOULOULOULOULOULOULOULOULOULOULU");
        
    }
}
