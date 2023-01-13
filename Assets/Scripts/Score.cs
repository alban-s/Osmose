using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Osmose.Game{
    public class Score : MonoBehaviour{
    public int nb_equipe = 2;
    public ushort score_equipe_max = 10000;
    public ushort score_equipe;
    public int nb_joueur_totale;
    public int nb_joueur_equipe;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ComputeScoreEquipe(score_equipe,joueurs);
    }
    void ComputeScoreEquipe(ushort score_equipe,  GameObject[] joueurs){
        for(GameObject objet in joueurs){
            score_equipe += objet.GetPoints();
        }
    }
    ushort GetScoreEquipe(){
        return score_equipe;
    }

    


}

}
