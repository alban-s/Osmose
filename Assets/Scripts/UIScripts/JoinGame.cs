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

        GameManager manager;

        IEnumerator wait_and_close()
        {
            yield return new WaitForSeconds(0.5f);
            thisWindow.SetActive(false);
        }

        void Start()
        {
            manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public void JoinServeur()
        {
            int remaining = manager.GetMaxNbOfPlayer() - manager.GetPlayersReadyCount();
            if (!NetworkClient.active && remaining != 0)
            {
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
