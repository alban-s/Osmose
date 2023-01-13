using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour
{
    public GameObject thisWindow;
    public GameObject persoSelectionWindow;

    private int nbPlayers = 4;
    private int nbPtsPerTeam = 1000;
    private double gameTime = 2.0;

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
            nbPtsPerTeam.ToString()+" et gameTime =" + gameTime.ToString(".##"));

        thisWindow.SetActive(false);
        persoSelectionWindow.SetActive(true);
    }

    public void ReturnMenu()
    {
        thisWindow.SetActive(false);
    }
}
