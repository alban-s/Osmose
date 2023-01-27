using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;
     private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            //AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

        }
        if (!isServer)
        {
            GetComponent<NetworkTransformReliable>().syncDirection = SyncDirection.ClientToServer;
        }


    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();

        GameManager.RegisterPlayer(netId, gameObject);
    }

    private void DisableComponents()
    {
        // On va boucler sur les diff�rents composants renseign�s et les d�sactiver si ce joueur n'est pas le notre
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
       
    }

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnregisterPlayer(transform.name);
    }
}
