using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osmose.Game
{
    public enum TeamColour
    {
        Red,
        Blue
    }


    public class Team : MonoBehaviour
    {
        
        public TeamColour team;
        // Start is called before the first frame update
        void Start()
        {
            SetTeam(team);
            ChoixEquipe();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetTeam(TeamColour team)
        {
            this.team = team;
        }

        public TeamColour GetTeam()
        {
            return team;
        }

        public bool IsSameTeam(TeamColour team)
        {
            return this.team == team;
        }

        void ChoixEquipe()
        {
            if (team == TeamColour.Red)
            {
                GetComponent<Renderer>().material.color = Color.red;

            }
            if (team == TeamColour.Blue)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}