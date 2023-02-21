using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using Mirror;

public class CreateGame : MonoBehaviour
{
    public NetworkManager manager;

    public GameObject thisWindow;
    public GameObject persoSelectionWindow;

    private GameObject gameManager;
    private ushort nbPlayers;
    private ushort nbPtsPerTeam;
    private ushort gameTime;

    void Start()
    {
        nbPlayers = 10;
        nbPtsPerTeam = 10000;
        gameTime = 15;
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void InitNbPlayers(string value)
    {
        nbPlayers = ushort.Parse(value);
    }

    public void InitPtsPerTeam(string value)
    {
        nbPtsPerTeam = ushort.Parse(value);
    }

    public void InitGameTime(string minutes)
    {
        gameTime = ushort.Parse(minutes);
    }


    public void LaunchGame()
    {
        print("serveurToLink\n");
        print("nb Joueurs =" + nbPlayers.ToString() + ", Points par �quipes =" +
            nbPtsPerTeam.ToString() + " et gameTime =" + gameTime.ToString());
           
        manager.StartHost();


        gameManager.GetComponent<GameManager>().SetMaxNbOfPlayer(nbPlayers);
        gameManager.GetComponent<Score>().SetTeamMaxScore(nbPtsPerTeam);
        gameManager.GetComponent<Timer>().SetDuration(gameTime);

        
        string host = Dns.GetHostName();
        Console.WriteLine("Le nom de l'hote est :" + host);

        string ip = Dns.GetHostEntry(host).AddressList[1].ToString();
        print("Mon adresse IP est :" + ip);
        thisWindow.SetActive(false);
        persoSelectionWindow.SetActive(true);
    }

    public void ReturnMenu()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
            manager.StopHost();
        thisWindow.SetActive(false);
    }
}
