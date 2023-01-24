using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;

namespace Mirror
{
    public class CreateGame : MonoBehaviour
    {
        public NetworkManager manager;

        public GameObject thisWindow;
        public GameObject persoSelectionWindow;

        private int nbPlayers = 4;
        private int nbPtsPerTeam = 1000;
        private double gameTime = 2.0;

        void Awake()
        {
            //manager = GetComponent<NetworkManager>();
        }
        public void InitNbPlayers(string value)
        {
            nbPlayers = int.Parse(value); // Conversion pas propre a reprendre
        }

        public void InitPtsPerTeam(string value)
        {
            nbPtsPerTeam = int.Parse(value); // Conversion pas propre a reprendre
        }

        public void InitGameTime(string value)
        {
            gameTime = double.Parse(value);// Conversion pas propre a reprendre
        }


        public void LaunchGame()
        {
            print("serveurToLink\n");
            print("nb Joueurs =" + nbPlayers.ToString() + ", Points par équipes =" +
                nbPtsPerTeam.ToString() + " et gameTime =" + gameTime.ToString(".##"));
           
            manager.StartHost();
            string host = Dns.GetHostName();
            Console.WriteLine("Le nom de l'hôte est :" + host);
            // Récupérer l'adresse IP
            string ip = Dns.GetHostEntry(host).AddressList[1].ToString();
            print("Mon adresse IP est :" + ip);
            thisWindow.SetActive(false);
            persoSelectionWindow.SetActive(true);
        }

        public void ReturnMenu()
        {
            thisWindow.SetActive(false);
        }
    }
}
