using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Osmose.Gameplay;


public class Score : NetworkBehaviour
{
    public int nb_equipe = 2;
    public int min_score_player = 100;

    public GameObject[] player_list_blue;
    public GameObject[] player_list_red;

    [SyncVar]
    public int team_max_score;
    [SyncVar]
    public int team_red_score = 0;
    [SyncVar]
    public int team_blue_score = 0;
    [SyncVar]
    public int remaining_score_blue;
    [SyncVar]
    public int remaining_score_red;

    // Start is called before the first frame update
    void Start()
    {
        player_list_blue = GetComponent<GameManager>().GetPlayerBlueList().ToArray();
        player_list_red = GetComponent<GameManager>().GetPlayerRedList().ToArray();
        remaining_score_blue = team_max_score;
        remaining_score_red = team_max_score;
        team_blue_score = ComputeScoreEquipe(player_list_blue);
        team_red_score = ComputeScoreEquipe(player_list_red);
    }

    // Update is called once per frame
    void Update()
    {
        player_list_blue = GetComponent<GameManager>().GetPlayerBlueList().ToArray();
        player_list_red = GetComponent<GameManager>().GetPlayerRedList().ToArray();
        team_blue_score = ComputeScoreEquipe(player_list_blue);
        team_red_score = ComputeScoreEquipe(player_list_red);
    }

    int ComputeScoreEquipe(GameObject[] player_list)
    {
        int score_equipe = 0;
        foreach (GameObject objet in player_list)
        {
            score_equipe += objet.GetComponent<Health>().CurrentPoints;
        }
        return score_equipe;
    }

    [Command(requiresAuthority = false)]
    public void IncreaseScoreBlue(int score)
    {
        team_blue_score += score;
    }
    [Command(requiresAuthority = false)]
    public void IncreaseScoreRed(int score)
    {
        team_red_score += score;
    }
    [Command(requiresAuthority = false)]
    public void DecreaseScoreBlue(int score)
    {
        team_blue_score -= score;
    }
    [Command(requiresAuthority = false)]
    public void DecreaseScoreRed(int score)
    {
        team_red_score -= score;
    }

    public int GetTeamBlueScore()
    {
        return team_blue_score;
    }
    public int GetTeamRedScore()
    {
        return team_red_score;
    }

    [Command(requiresAuthority = false)]
    public void SetScoreRestantBlue(int score)
    {
        remaining_score_blue -= score;
    }

    public int GetScoreRestantBlue()
    {
        remaining_score_blue = (int)(team_max_score - team_blue_score - 100*(GetComponent<GameManager>().GetBluePlayersNotReadyCount()-1));
        return remaining_score_blue;
    }

    [Command(requiresAuthority = false)]
    public void SetScoreRestantRed(int score)
    {
        remaining_score_red -= score;
    }

    public int GetScoreRestantRed()
    {
        remaining_score_red = (int)(team_max_score - team_red_score - 100*(GetComponent<GameManager>().GetRedPlayersNotReadyCount()-1));
        return remaining_score_red;
    }

    public int GetTeamMaxScore()
    {
        return team_max_score;
    }

    public void SetTeamMaxScore(int value)
    {
        team_max_score = value;
    }

    public GameObject[] GetPlayersBlue()
    {
        return player_list_blue;
    }

    public GameObject[] GetPlayersRed()
    {
        return player_list_red;
    }
}

