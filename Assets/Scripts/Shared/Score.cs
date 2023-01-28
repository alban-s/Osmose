using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Osmose.Game;


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
    public ushort remaining_score;
/*    [SyncVar]
    public ushort[] players_score_blue;
    [SyncVar]
    public ushort[] players_score_red;*/

    // Start is called before the first frame update
    void Start()
    {
        remaining_score = team_max_score;
        ComputeScoreEquipe(team_blue_score, player_list_blue);
        ComputeScoreEquipe(team_red_score, player_list_red);
    }

    // Update is called once per frame
    void Update()
    {
        // ComputeScoreEquipe(score_equipe, player_list);
    }

    void ComputeScoreEquipe(ushort score_equipe, GameObject[] player_list)
    {
        foreach (GameObject objet in player_list)
        {
            score_equipe += objet.GetComponent<Health>().GetPoints();
        }
    }

    [Command]
    public void IncreaseScoreBlue(ushort score)
    {
        team_blue_score += score;
    }
    [Command]
    public void IncreaseScoreRed(ushort score)
    {
        team_red_score += score;
    }
    [Command]
    public void DecreaseScoreBlue(ushort score)
    {
        team_blue_score -= score;
    }
    [Command]
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

    [Command]
    void SetScoreRestant(ushort score)
    {
        remaining_score -= score;
    }

    ushort GetScoreRestant()
    {
        return remaining_score;
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

