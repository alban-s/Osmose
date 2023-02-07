using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osmose.Gameplay
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
        public Material material_blue;
        public Material material_red;
        void Start()
        {
            SetTeam(team);
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

        public void ChoixEquipe()
        {
            if (CompareTag("Player"))
            {
                if (team == TeamColour.Red)
                {
                    transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = material_red;
                    transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>().material = material_red;

                }
                if (team == TeamColour.Blue)
                {
                    transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = material_blue;
                    transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>().material = material_blue;
                }
            }
        }
    }
}