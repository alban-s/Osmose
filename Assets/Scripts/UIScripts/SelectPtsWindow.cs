using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Osmose.Game;
using TMPro;

public class SelectPtsWindow : MonoBehaviour
{
    public GameObject thisWindow;
    private GameObject manager;
    private GameObject player;
    public TextMeshProUGUI displayTotalTeamPoints;
    public TextMeshProUGUI displayCurTeamPoints;
    public TextMeshProUGUI selectedPts;

    private TeamColour playerTeam;
    private int teamPoints;
    private int curTeamPoints;
    private int playerPts;



    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        playerPts = 100;
        playerTeam = player.GetComponent<Team>().GetTeam();
        if(playerTeam == TeamColour.Blue)
        {
            teamPoints = manager.GetComponent<Score>().GetTeamBlueScore();
            displayTotalTeamPoints.SetText(teamPoints.ToString());
        }
        else
        {
            teamPoints = manager.GetComponent<Score>().GetTeamRedScore();
            displayTotalTeamPoints.SetText(teamPoints.ToString());
        }

        Update();
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTeam == TeamColour.Blue)
        {
            curTeamPoints = manager.GetComponent<Score>().GetScoreRestantBlue();
            displayCurTeamPoints.SetText(curTeamPoints.ToString());
        }
        else
        {
            curTeamPoints = manager.GetComponent<Score>().GetScoreRestantRed();
            displayCurTeamPoints.SetText(curTeamPoints.ToString());
        }
    }

    public void OnSelectClicked()
    {
        Debug.Log("baé"+selectedPts.text+"alors");
        ushort score = 0;
        try
        {
            score = ushort.Parse(selectedPts.text);
            Debug.Log(score);
        }
        catch (FormatException)
        {
            score = 100;
            Debug.Log($"Unable to parse '{selectedPts.text}'");
        }
        /*        if (ushort.TryParse(selectedPts.text, out score))
                    System.Console.WriteLine(score);*/
        //ushort.TryParse(selectedPts.text, out score);
        thisWindow.SetActive(false);
        if (playerTeam == TeamColour.Blue)
        {
            manager.GetComponent<Score>().SetScoreRestantBlue(score);
            manager.GetComponent<Score>().IncreaseScoreBlue(score);
        }
        else
        {
            manager.GetComponent<Score>().SetScoreRestantRed(score);
            manager.GetComponent<Score>().IncreaseScoreRed(score);
        }
        player.GetComponent<Team>().GetTeam();
    }


    public void PointsSelected(string point)
    {
        int selected = int.Parse(point);

        if (selected < 100)
        {
            playerPts = 100;
        }else if(selected > curTeamPoints)
        {
            playerPts = curTeamPoints;
        }
        else
        {
            playerPts = selected;
        }
    }

}
