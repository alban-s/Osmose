using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerCustom : NetworkManager
{
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
        Debug.Log("OULOULOULOULOULOULOULOULOULOULOULOULOULOULOULU");
    }
}
