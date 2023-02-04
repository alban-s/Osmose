using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Osmose.Game;
using TMPro;

public class EndGameWindow : MonoBehaviour
{
    public GameObject data;
    public Scene GameScene;

    public TextMeshProUGUI displayWinnerTeam;
    public TextMeshProUGUI displayLoserTeam;
    public TextMeshProUGUI displayWinnerTeamPoints;
    public TextMeshProUGUI displayLoserTeamPoints;
    public TextMeshProUGUI displayWinnerGamersPoints;
    public TextMeshProUGUI displayLosersGamersPoints;

    // Start is called before the first frame update
    void Start()
    {
        // On récupère tous les scores (Equipes + joueurs)
        int redScore = data.GetComponent<Score>().GetTeamRedScore();
        int blueScore = data.GetComponent<Score>().GetTeamBlueScore();

        GameObject[] players_red = data.GetComponent<Score>().GetPlayersRed();
        GameObject[] players_blue = data.GetComponent<Score>().GetPlayersBlue();

        string teamBlueScore = "";
        string teamRedScore = "";

        foreach (GameObject objet in players_red)
        {
            teamRedScore += objet.transform.name;
            int score = objet.GetComponent<Health>().GetPoints();
            teamRedScore += " : ";
            teamRedScore += score.ToString();
            teamRedScore += "\\n";
        }

        foreach (GameObject objet in players_blue)
        {
            teamBlueScore += objet.transform.name;
            int score = objet.GetComponent<Health>().GetPoints();
            teamBlueScore += " : ";
            teamBlueScore += score.ToString();
            teamBlueScore += "\\n";
        }


        // On les display selon l'équipe gagnante
        if (redScore > blueScore)
        {
            displayWinnerTeam.SetText("Equipe Rouge");
            displayWinnerTeamPoints.SetText(redScore.ToString());
            displayWinnerGamersPoints.SetText(teamRedScore);

            displayLoserTeam.SetText("Equipe Bleu");
            displayLoserTeamPoints.SetText(blueScore.ToString());
            displayLosersGamersPoints.SetText(teamBlueScore);
        }
        else
        {
            displayWinnerTeam.SetText("Equipe Bleu");
            displayWinnerTeamPoints.SetText(blueScore.ToString());
            displayWinnerGamersPoints.SetText(teamBlueScore);

            displayLoserTeam.SetText("Equipe Rouge");
            displayLoserTeamPoints.SetText(redScore.ToString());
            displayLosersGamersPoints.SetText(teamRedScore);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
