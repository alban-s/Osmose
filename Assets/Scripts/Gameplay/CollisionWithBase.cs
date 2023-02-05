using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithBase : MonoBehaviour
    {
        //public bool HasReachedEnemyBase { get; set; }
        public bool isActive;
        private GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            //HasReachedEnemyBase = false;
            isActive = false;
            // get the parent of the object
            GameObject player = transform.parent.gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        //When Player is touching the base, set IsInBase to true, if they are !CanMatch, sets them to CanMatch
        void OnTriggerEnter(Collider other)
        {
            //Debug.Log("hey");
            //Debug.Log(gameObject.GetComponent<Health>().GetPoints());
            //Debug.Log(gameObject.GetComponent<Team>().GetTeam());
            //Debug.Log(other.gameObject.GetComponent<Team>().GetTeam());
            if (isActive)
            {
                if (other.gameObject.CompareTag("Base"))
                {
                    //if your base set IsInBase, and if !CanMatch, sets CanMatch to true, if enemy base and canMatch is true, get 10000 points
                    if (other.gameObject.GetComponent<Team>().GetTeam() == gameObject.GetComponent<Team>().GetTeam())
                    {
                        if (player.GetComponent<Health>().IsInBase == false)
                        {
                            player.GetComponent<Health>().IsInBase = true;
                            if (player.GetComponent<Health>().CanMatch == false)
                            {
                                player.GetComponent<Health>().CanMatch = true;
                            }
                            // set IsInBase
                            player.GetComponent<Health>().IsInBase = true;
                        }
                        Debug.Log(player.GetComponent<Health>().CanMatch);
                    }
                    else if (other.gameObject.GetComponent<Team>().GetTeam() != gameObject.GetComponent<Team>().GetTeam())
                    {
                        Debug.Log(player.GetComponent<Health>().CanMatch);
                        if (gameObject.GetComponent<Health>().CanMatch == true)// && HasReachedEnemyBase == false)
                        {
                            Debug.Log("hoy");
                            player.GetComponent<Health>().IncreasePoints(10000, other.gameObject);
                            //HasReachedEnemyBase = true;
                            player.GetComponent<Health>().CanMatch = false;
                        }
                    }
                }
            }
        }

        //When Player is not touching the base, set IsInBase to false
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Base"))
            {
                if (other.gameObject.GetComponent<Team>().GetTeam() == gameObject.GetComponent<Team>().GetTeam())
                {
                    player.GetComponent<Health>().IsInBase = false;
                    Debug.Log("Hey");
                }
            }
        }
    }
}