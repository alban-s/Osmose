using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Osmose.Gameplay;
using TMPro;
using System;

public class GameWindow : MonoBehaviour
{
    private GameObject GameManager;

    public TextMeshProUGUI gamerTeam;
    public TextMeshProUGUI oppositTeam;
    public TextMeshProUGUI gamerTeamScore;
    public TextMeshProUGUI oppositTeamScore;
    public TextMeshProUGUI gamerScore;
    public TextMeshProUGUI gameTime;

    private GameObject player;
    private Color32 teamBlue;
    private Color32 teamRed;



    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        teamBlue = new Color32(0, 0, 255, 255);
        teamRed = new Color32(255, 0, 0, 255);
        Update();
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    // Update is called once per frame
    void Update()
    {
        // Display the teams and there scores
        TeamColour curTeam = player.GetComponent<Team>().GetTeam();

        if(curTeam == TeamColour.Red)
        {
            gamerTeam.SetText("Rouge");
            gamerTeam.color = teamRed;

            oppositTeam.SetText("Bleu");
            oppositTeam.color = teamBlue;

            int curGamerTeamScore = GameManager.GetComponent<Score>().GetTeamRedScore();
            gamerTeamScore.SetText(curGamerTeamScore.ToString());

            int curOppositTeamScore = GameManager.GetComponent<Score>().GetTeamBlueScore();
            oppositTeamScore.SetText(curOppositTeamScore.ToString());
        }
        else
        {
            oppositTeam.SetText("Rouge");
            oppositTeam.color = teamRed;

            gamerTeam.SetText("Bleu");
            gamerTeam.color = teamBlue;

            int curGamerTeamScore = GameManager.GetComponent<Score>().GetTeamBlueScore();
            gamerTeamScore.SetText(curGamerTeamScore.ToString());

            int curOppositTeamScore = GameManager.GetComponent<Score>().GetTeamRedScore();
            oppositTeamScore.SetText(curOppositTeamScore.ToString());
        }

        // Display gamer score
        int curScore = player.GetComponent<Health>().GetPoints();
        gamerScore.SetText(curScore.ToString());
     

        // Display time
        float curTime = GameManager.GetComponent<Timer>().GetTime();
        gameTime.SetText((Math.Truncate(curTime / 60)).ToString() + ":" + (Math.Truncate(curTime % 60)).ToString());
    }
}

