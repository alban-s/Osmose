using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Osmose.Game;
using TMPro;

public class GameWindow : MonoBehaviour
{
    public GameObject joueur;
    public GameObject data;

    public TextMeshProUGUI gamerTeam;
    public TextMeshProUGUI oppositTeam;
    public TextMeshProUGUI gamerTeamScore;
    public TextMeshProUGUI oppositTeamScore;
    public TextMeshProUGUI gamerScore;
    public TextMeshProUGUI gameTime;

    private Color32 teamBlue;
    private Color32 teamRed;



    // Start is called before the first frame update
    void Start()
    {
        teamBlue = new Color32(255, 0, 0, 255);
        teamRed = new Color32(255, 255, 0, 0);
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        // Display the teams and there scores
        TeamColour curTeam = joueur.GetComponent<Team>().GetTeam();

        if(curTeam == TeamColour.Red)
        {
            gamerTeam.SetText("Rouge");
            gamerTeam.color = teamBlue;

            oppositTeam.SetText("Bleu");
            oppositTeam.color = teamBlue;
        }
        else
        {
            oppositTeam.SetText("Rouge");
            oppositTeam.color = teamBlue;

            gamerTeam.SetText("Bleu");
            gamerTeam.color = teamBlue;
        }

        int curGamerTeamScore = data.GetComponent<Score>().GetScoreEquipe();
        gamerTeamScore.SetText(curGamerTeamScore.ToString());

        int curOppositTeamScore = data.GetComponent<Score>().GetScoreEquipe();
        oppositTeamScore.SetText(curGamerTeamScore.ToString());

        // Display gamer score
        int curScore = joueur.GetComponent<Health>().GetPoints();
        gamerScore.SetText(curScore.ToString());
     

        // Display time
        float curTime = data.GetComponent<Timer>().GetTime();
        gameTime.SetText(curTime.ToString(".##"));

    }
}

