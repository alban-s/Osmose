using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithBase : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //When Player is touching the base, set IsInBase to true, if they are !CanMatch, sets them to CanMatch
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Base"))
            {
                //if your base set IsInBase, and if !CanMatch, sets CanMatch to true, if enemy base and canMatch is true, get 10000 points
                if (other.gameObject.GetComponent <Team>().GetTeam() == gameObject.GetComponent<Team>().GetTeam())
                {
                    if (gameObject.GetComponent<Health>().IsInBase == false)
                    {
                        gameObject.GetComponent<Health>().IsInBase = true;
                        if (gameObject.GetComponent<Health>().CanMatch == false)
                        {
                            gameObject.GetComponent<Health>().CanMatch = true;
                        }
                        else if (gameObject.GetComponent<Health>().CanMatch == true)
                        {
                            gameObject.GetComponent<Health>().IncreasePoints(10000, other.gameObject);
                        }
                    }
                }
                else if (other.gameObject.GetComponent<Team>().GetTeam() != gameObject.GetComponent<Team>().GetTeam())
                {
                    if (gameObject.GetComponent<Health>().CanMatch == true)
                    {
                        gameObject.GetComponent<Health>().IncreasePoints(10000, other.gameObject);
                    }
                }
            }
        }

        //When Player is not touching the base, set IsInBase to false
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Base"))
            {
                gameObject.GetComponent<Health>().IsInBase = false;
            }
        }
    }
}

