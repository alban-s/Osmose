using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Osmose.Game{
    public class Score : MonoBehaviour
    {
        public int nb_equipe = 2;
        public ushort score_equipe_max = 10000;
        public ushort score_min = 100;
        public ushort score_equipe;
        public ushort nb_joueur_totale;
        public ushort nb_joueur_equipe;
        public GameObject [] player_list;

        // Start is called before the first frame update
        void Start()
        {
            ComputeScoreEquipe(score_equipe,player_list);
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

        public void IncreaseScore(ushort score)
        {
            score_equipe += score;
        }

        public void DecreaseScore(ushort score)
        {
            score_equipe -= score;
        }

        public ushort GetScoreEquipe()
        {
            return score_equipe;
        }
    }
}
