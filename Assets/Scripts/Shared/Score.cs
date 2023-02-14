using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Osmose.Gameplay;


public class Score : NetworkBehaviour
{
    public int nb_equipe = 2;
    public ushort min_score_player = 100;

    public GameObject[] player_list_blue;
    public GameObject[] player_list_red;

    [SyncVar]
    public ushort team_max_score;
    [SyncVar]
    public ushort team_red_score = 0;
    [SyncVar]
    public ushort team_blue_score = 0;
    [SyncVar]
    public ushort remaining_score_blue;
    [SyncVar]
    public ushort remaining_score_red;

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

    ushort ComputeScoreEquipe(GameObject[] player_list)
    {
        ushort score_equipe = 0;
        foreach (GameObject objet in player_list)
        {
            score_equipe += objet.GetComponent<Health>().CurrentPoints;
        }
        return score_equipe;
    }

    [Command(requiresAuthority = false)]
    public void IncreaseScoreBlue(ushort score)
    {
        team_blue_score += score;
    }
    [Command(requiresAuthority = false)]
    public void IncreaseScoreRed(ushort score)
    {
        team_red_score += score;
    }
    [Command(requiresAuthority = false)]
    public void DecreaseScoreBlue(ushort score)
    {
        team_blue_score -= score;
    }
    [Command(requiresAuthority = false)]
    public void DecreaseScoreRed(ushort score)
    {
        team_red_score -= score;
    }

    public ushort GetTeamBlueScore()
    {
        return team_blue_score;
    }
    public ushort GetTeamRedScore()
    {
        return team_red_score;
    }

    [Command(requiresAuthority = false)]
    public void SetScoreRestantBlue(ushort score)
    {
        remaining_score_blue -= score;
    }

    public ushort GetScoreRestantBlue()
    {
        remaining_score_blue = (ushort)(team_max_score - team_blue_score);
        return remaining_score_blue;
    }

    [Command(requiresAuthority = false)]
    public void SetScoreRestantRed(ushort score)
    {
        remaining_score_red -= score;
    }

    public ushort GetScoreRestantRed()
    {
        remaining_score_red = (ushort)(team_max_score - team_red_score);
        return remaining_score_red;
    }

    public ushort GetTeamMaxScore()
    {
        return team_max_score;
    }

    public void SetTeamMaxScore(ushort value)
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

