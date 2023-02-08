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
    public InputField playerNb;
    public InputField maxTeamPoints;
    public InputField gameDuration;

    private GameObject gameManager;
    private ushort nbPlayers;
    private ushort nbPtsPerTeam;
    private float gameTime;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void InitNbPlayers(string value)
    {
        nbPlayers = ushort.Parse(value); // Conversion pas propre a reprendre
    }

    public void InitPtsPerTeam(string value)
    {
        nbPtsPerTeam = ushort.Parse(value); // Conversion pas propre a reprendre
    }

    public void InitGameTime(string value)
    {
        gameTime = float.Parse(value);// Conversion pas propre a reprendre
    }


    public void LaunchGame()
    {
        print("serveurToLink\n");
        print("nb Joueurs =" + nbPlayers.ToString() + ", Points par �quipes =" +
            nbPtsPerTeam.ToString() + " et gameTime =" + gameTime.ToString(".##"));
           
        manager.StartHost();

        InitNbPlayers(playerNb.text);
        InitPtsPerTeam(maxTeamPoints.text);
        InitGameTime(gameDuration.text);

        gameManager.GetComponent<GameManager>().SetMaxNbOfPlayer(nbPlayers);
        gameManager.GetComponent<Score>().SetTeamMaxScore(nbPtsPerTeam);
        gameManager.GetComponent<Timer>().SetDuration(gameTime);

        
        string host = Dns.GetHostName();
        Console.WriteLine("Le nom de l'h�te est :" + host);

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
