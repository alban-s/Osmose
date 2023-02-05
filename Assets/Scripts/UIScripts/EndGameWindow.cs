using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Osmose.Game;
using TMPro;

public class EndGameWindow : MonoBehaviour
{
    private GameObject GameManager;
    public Scene GameScene;

    public TextMeshProUGUI displayWinnerTeam;
    public TextMeshProUGUI displayLoserTeam;
    public TextMeshProUGUI displayWinnerTeamPoints;
    public TextMeshProUGUI displayLoserTeamPoints;
    public TextMeshProUGUI displayWinnerGamersPoints;
    public TextMeshProUGUI displayLosersGamersPoints;

    private List<GameObject> players_red, players_blue;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        // On récupère tous les scores (Equipes + joueurs)
        int redScore = GameManager.GetComponent<Score>().GetTeamRedScore();
        int blueScore = GameManager.GetComponent<Score>().GetTeamBlueScore();

        players_red = GameManager.GetComponent<GameManager>().GetPlayerRedList();
        players_blue = GameManager.GetComponent<GameManager>().GetPlayerBlueList();

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
