using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Osmose.Gameplay;
using TMPro;

public class SelectPtsWindow : MonoBehaviour
{
    public GameObject thisWindow;
    private GameObject manager;
    private GameObject player;
    public TextMeshProUGUI displayTotalTeamPoints;
    public TextMeshProUGUI displayCurTeamPoints;
    public InputField selectedPts;

    private TeamColour playerTeam;
    private ushort teamPoints;
    private ushort curTeamPoints;
    private ushort playerPts;



    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        playerTeam = player.GetComponent<Team>().GetTeam();
        if(playerTeam == TeamColour.Blue)
        {
            teamPoints = manager.GetComponent<Score>().GetTeamMaxScore();
            displayTotalTeamPoints.SetText(teamPoints.ToString());
            curTeamPoints = manager.GetComponent<Score>().GetScoreRestantBlue();
            displayCurTeamPoints.SetText(curTeamPoints.ToString());
        }
        else
        {
            teamPoints = manager.GetComponent<Score>().GetTeamMaxScore();
            displayTotalTeamPoints.SetText(teamPoints.ToString());
            curTeamPoints = manager.GetComponent<Score>().GetScoreRestantRed();
            displayCurTeamPoints.SetText(curTeamPoints.ToString());
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
        // Debug.Log(manager.GetComponent<Score>().GetScoreRestantRed());
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
        PointsSelected();
        
        if (playerTeam == TeamColour.Blue)
        {
            manager.GetComponent<Score>().SetScoreRestantBlue(playerPts);
            manager.GetComponent<Score>().IncreaseScoreBlue(playerPts);
        }
        else
        {
            manager.GetComponent<Score>().SetScoreRestantRed(playerPts);
            manager.GetComponent<Score>().IncreaseScoreRed(playerPts);
        }
        player.GetComponent<Health>().SetStartPoints(playerPts);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        thisWindow.SetActive(false);
    }


    public void PointsSelected()
    {
        ushort.TryParse(selectedPts.text, out ushort selected);

        if (selected < 100)
        {
            playerPts = 100;
        }
        else if(selected > curTeamPoints)
        {
            playerPts = curTeamPoints;
        }
        else
        {
            playerPts = selected;
        }
    }

}
