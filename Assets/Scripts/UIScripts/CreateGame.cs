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
    private int nbPlayers;
    private int nbPtsPerTeam;
    private int gameTime;

    void Start()
    {
        nbPlayers = 10;
        nbPtsPerTeam = 10000;
        gameTime = 15;
    }

    public void InitNbPlayers(string value)
    {
        nbPlayers = int.Parse(value);
    }

    public void InitPtsPerTeam(string value)
    {
        int testMaxUnshort = int.Parse(value);

        if(testMaxUnshort > 65000)
        {
            nbPtsPerTeam = 65000;
        }
        else
        {
            nbPtsPerTeam = int.Parse(value);
        }
    }

    public void InitGameTime(string minutes)
    {
        gameTime = int.Parse(minutes);
    }


    public void LaunchGame()
    {
        print("serveurToLink\n");
        print("nb Joueurs =" + nbPlayers.ToString() + ", Points par �quipes =" +
            nbPtsPerTeam.ToString() + " et gameTime =" + gameTime.ToString());
           
        manager.StartHost();

        gameManager = GameObject.Find("GameManager");
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
