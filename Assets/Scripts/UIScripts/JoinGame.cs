using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Mirror
{
    public class JoinGame : MonoBehaviour
    {
        public NetworkManager manager;
        public GameObject thisWindow;
        public GameObject persoSelectionWindow;
        public TextMeshProUGUI adresseIP;

        IEnumerator wait_and_close()
    {
        yield return new WaitForSeconds(0.5);
        thisWindow.SetActive(false);
    }

        // void Awake()
        // {
        //     manager = GetComponent<NetworkManager>();
        // }

        public void JoinServeur()
        {
            if (!NetworkClient.active){
                print("ip serveur : " + adresseIP.text);
            
                manager.networkAddress = adresseIP.text;
                manager.StartClient();

                StartCoroutine(wait_and_close());
                persoSelectionWindow.SetActive(true);
            }
            
        }

        public void ReturnMenu()
        {
            thisWindow.SetActive(false);
        }
    }
}
