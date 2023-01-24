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

        public void JoinServeur()
        {
            print("ip serveur : " + adresseIP.text);
            manager.networkAddress = adresseIP.text;
            
            manager.StartClient();
            
            thisWindow.SetActive(false);
            //persoSelectionWindow.SetActive(true);
        }

        public void ReturnMenu()
        {
            thisWindow.SetActive(false);
        }
    }
}
