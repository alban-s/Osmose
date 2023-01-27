using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Osmose.Game{
    public class Score : MonoBehaviour
    {
        public int nb_equipe = 2;
        public ushort score_equipe_max = 10000;
        public ushort score_equipe_blue = 0;
        public ushort score_min_joueur = 100;
        public ushort score_equipe_red = 0;
        public ushort nb_joueur_totale;
        public ushort nb_joueur_equipe;
        public ushort score_restant;
        public ushort score_courant;
        public ushort [] scores_joueurs_blue;
        public ushort [] scores_joueurs_red;
        public GameObject [] player_list_blue;
        public GameObject [] player_list_red;

        // Start is called before the first frame update
        void Start()
        {
            ComputeScoreEquipe(score_equipe_blue,player_list_blue);
            ComputeScoreEquipe(score_equipe_red,player_list_red);
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

        public void IncreaseScoreBlue(ushort score)
        {
            score_equipe_blue += score;
        }
        public void IncreaseScoreRed(ushort score)
        {
            score_equipe_red += score;
        }

        public void DecreaseScoreBlue(ushort score)
        {
            score_equipe_blue -= score;
        }

        public void DecreaseScoreRed(ushort score)
        {
            score_equipe_red -= score;
        }

        ushort getScoreEquipeBlue(){
            return score_equipe_blue;
        }
        ushort getScoreEquipeRed(){
            return score_equipe_red;
        }

        GameObject [] getPlayerListBlue(){
            return player_list_blue;
        }

        GameObject [] getPlayerListRed(){
            return player_list_red;
        }

        void setScoreRestant(ushort score){
            score_restant = score_restant - score;
        }
        ushort getScoreCourant(){
            return score_courant;
        }
        ushort getScoreRestant(){
            return score_restant;
        }

        void setScorePlayerEquipeBlue(){
            for(int i = 0 ; i < player_list_blue.length();i++){
                score_equipe_blue[i] = player_list_blue[i].GetPoints(); 
            }
        }

        void setScorePlayerEquipeRed(){
            for(int i = 0 ; i < player_list_red.length();i++){
                score_equipe_red[i] = player_list_red[i].GetPoints(); 
            }
        }
       

    }
}
