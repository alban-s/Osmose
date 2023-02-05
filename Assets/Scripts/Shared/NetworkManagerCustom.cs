using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerCustom : NetworkManager
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        gameManager.GetComponent<GameManager>().update_names();
        Debug.Log("OULOULOULOULOULOULOULOULOULOULOULOULOULOULOULU");
        
    }
}
