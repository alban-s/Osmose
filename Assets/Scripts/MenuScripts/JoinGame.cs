using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Mirror
{
    [RequireComponent(typeof(NetworkManager))]
    public class JoinGame : MonoBehaviour
    {
        NetworkManager manager;
        public GameObject thisWindow;
        public GameObject persoSelectionWindow;
        public TextMeshProUGUI adresseIP;


        void Awake(){
            manager = GetComponent<NetworkManager>();
        }

        public void JoinServeur()
        {
            if (!NetworkClient.active){
                print("ip serveur : " + adresseIP.text);
            
            
                manager.StartClient();

                manager.networkAddress = adresseIP.text;

                thisWindow.SetActive(false);
            //persoSelectionWindow.SetActive(true);
            }
            
        }

        public void ReturnMenu()
        {
            thisWindow.SetActive(false);
        }
    }
}
