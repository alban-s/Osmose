using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osmose.Game
{
    public class Team : MonoBehaviour
    {
        public enum TeamColour
        {
            Red,
            Blue
        }
        public TeamColour team;
        // Start is called before the first frame update
        void Start()
        {
            SetTeam(TeamColour.Red);
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
    }
}