using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int nb_equipe = 2;
    private int num_equipe;
    private int score_max_equipe = 10000;
    private int score_equipe;
    private int score_joueur;
    private int[] score_joueurs;
    private int nb_joueur_totale;
    private int nb_joueur_equipe;
    public int score;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetNumEquipe(){
        return num_equipe;
    }
    int GetScoreJoueur(){
        return score_joueur;
    }

    void SetScoreEquipe(int score_joueurs, int score_equipe){
        score_equipe -= score_joueur;
    }

    int GetScoreEquipe(){
        return score_equipe;
    }


}
