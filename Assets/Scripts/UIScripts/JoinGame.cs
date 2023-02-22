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

        GameObject gameManager;

        IEnumerator wait_and_close()
        {
            yield return new WaitForSeconds(0.5f);
            thisWindow.SetActive(false);
        }

        public void JoinServeur()
        {
            
            if (!NetworkClient.active)
            {
                print("ip serveur : " + adresseIP.text);

                NetworkManager.singleton.networkAddress = adresseIP.text;
                NetworkManager.singleton.StartClient();

                gameManager = GameObject.Find("GameManager");
                int remaining = gameManager.GetComponent<GameManager>().GetMaxNbOfPlayer() - gameManager.GetComponent<GameManager>().GetPlayersReadyCount();
                if(remaining != 0 || gameManager.GetComponent<Timer>().GameOn())
                {
                    NetworkManager.singleton.StopClient();
                }
                else
                {
                    StartCoroutine(wait_and_close());
                    persoSelectionWindow.SetActive(true);
                }
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
