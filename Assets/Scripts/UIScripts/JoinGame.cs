using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mirror
{
    public class JoinGame : MonoBehaviour
    {
        public GameObject thisWindow;
        public GameObject persoSelectionWindow;
        //public TextMeshProUGUI adresseIP;
        public InputField adresseIP;

        IEnumerator wait_and_close()
    {
        yield return new WaitForSeconds(0.5f);
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

                NetworkManager.singleton.networkAddress = adresseIP.text;
                NetworkManager.singleton.StartClient();
                

                StartCoroutine(wait_and_close());
                persoSelectionWindow.SetActive(true);
            }
            
        }

        public void ReturnMenu()
        {
            if (NetworkClient.isConnected)
                NetworkManager.singleton.StopClient();
            thisWindow.SetActive(false);
        }
    }
}
