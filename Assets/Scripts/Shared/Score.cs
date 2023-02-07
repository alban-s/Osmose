using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Osmose.Gameplay;


public class Score : NetworkBehaviour
{
    public int nb_equipe = 2;
    public ushort team_max_score = 10000;
    public ushort min_score_player = 100;

    public GameObject[] player_list_blue;
    public GameObject[] player_list_red;

    [SyncVar]
    public ushort team_red_score = 0;
    [SyncVar]
    public ushort team_blue_score = 0;
    [SyncVar]
    public ushort remaining_score_blue;
    [SyncVar]
    public ushort remaining_score_red;
/*    [SyncVar]
    public ushort[] players_score_blue;
    [SyncVar]
    public ushort[] players_score_red;*/

    // Start is called before the first frame update
    void Start()
    {
        player_list_blue = GetComponent<GameManager>().GetPlayerRedList().ToArray();
        player_list_blue = GetComponent<GameManager>().GetPlayerRedList().ToArray();
        remaining_score_blue = team_max_score;
        remaining_score_red = team_max_score;
        ComputeScoreEquipe(team_blue_score, player_list_blue);
        ComputeScoreEquipe(team_red_score, player_list_red);
    }

    // Update is called once per frame
    void Update()
    {
        player_list_blue = GetComponent<GameManager>().GetPlayerRedList().ToArray();
        player_list_blue = GetComponent<GameManager>().GetPlayerRedList().ToArray();
        ComputeScoreEquipe(team_blue_score, player_list_blue);
        ComputeScoreEquipe(team_red_score, player_list_red);
    }

    void ComputeScoreEquipe(ushort score_equipe, GameObject[] player_list)
    {
        foreach (GameObject objet in player_list)
        {
            score_equipe += objet.GetComponent<Health>().GetPoints();
        }
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
        return remaining_score_blue;
    }

    [Command(requiresAuthority = false)]
    public void SetScoreRestantRed(ushort score)
    {
        remaining_score_red -= score;
    }

    public ushort GetScoreRestantRed()
    {
        return remaining_score_red;
    }

    public ushort GetTeamMaxScore()
    {
        return team_max_score;
    }

    public GameObject[] GetPlayersBlue()
    {
        return player_list_blue;
    }

    public GameObject[] GetPlayersRed()
    {
        return player_list_red;
    }

    /*    void SetScorePlayerEquipeBlue()
        {
            for (int i = 0; i < player_list_blue.Length; i++)
            {
                players_score_blue[i] = player_list_blue[i].GetComponent<Health>().GetPoints();
            }
        }

        void SetScorePlayerEquipeRed()
        {
            for (int i = 0; i < player_list_red.Length; i++)
            {
                players_score_red[i] = player_list_red[i].GetComponent<Health>().GetPoints();
            }
        }*/
}

