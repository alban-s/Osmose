using UnityEngine;
using Mirror;
using Osmose.Game;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    GameObject initValueGUI;

    [SerializeField]
    private GameObject GameWindowPrefab;
    private GameObject GameWindowInstance;

    Camera sceneCamera;
     private void Start()
    {
        initValueGUI = GameObject.Find("InitPersoPanel");
        //Debug.LogError(initValueGUI.name);
        
        if (!isLocalPlayer)
        {
            DisableComponents();
            //AssignRemoteLayer();
        }
        else
        {
            InitClientValues();

            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            GameWindowInstance = Instantiate(GameWindowPrefab);

            GameWindow ui = GameWindowInstance.GetComponent<GameWindow>();

            if(ui == null)
            {
                Debug.LogError("Pas de gamewindow sur gamewindowinstance");
            }
            else
            {
                ui.SetPlayer(gameObject);
            }
        }
        if (!isServer)
        {
            GetComponent<NetworkTransformReliable>().syncDirection = SyncDirection.ClientToServer;
        }

        
    }

    [Command]
    public void InitClientValues()
    {
        if (initValueGUI != null){
            InitPerso ip = initValueGUI.GetComponent<InitPerso>();
            if (ip != null){
                gameObject.GetComponent<Team>().team = (TeamColour)ip.choseTeam;
                gameObject.transform.name = ip.chosePseudo;
            }
        }
        InitClientLocalValues();
    }

    [ClientRpc]
    public void InitClientLocalValues(){
        if (initValueGUI != null){
            InitPerso ip = initValueGUI.GetComponent<InitPerso>();
            if (ip != null){
                gameObject.GetComponent<Team>().team = (TeamColour)ip.choseTeam;
                gameObject.transform.name = ip.chosePseudo;
            }
        }
    }
    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();

        if (initValueGUI != null)
        {
            InitPerso ip = initValueGUI.GetComponent<InitPerso>();
            if (ip != null)
            {
                GameManager.RegisterPlayer(ip.chosePseudo, gameObject);
               /* gameObject.GetComponent<Team>().team = (TeamColour)ip.choseTeam;
                gameObject.transform.name = ip.chosePseudo;*/
            }
        }

       
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
        Destroy(GameWindowInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnregisterPlayer(transform.name);
    }
}
