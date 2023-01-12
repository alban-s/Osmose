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
        
        // public int num_equipe ;

        
       
        public TeamColour teamcolor ;
        // Start is called before the first frame update
        void Start()
        {
            //SetTeam(TeamColour.Red);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetTeam(TeamColour teamcolor)
        {
            this.teamcolor = teamcolor;
        }

        public TeamColour GetTeam()
        {
            return teamcolor;
        }
    }
}