using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithBase : MonoBehaviour
    {
        public bool HasReachedEnemyBase { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            HasReachedEnemyBase = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        //When Player is touching the base, set IsInBase to true, if they are !IsActive, sets them to IsActive
        void OnTriggerEnter(Collider other)
        {
            //Debug.Log("hey");
            //Debug.Log(gameObject.GetComponent<Health>().GetPoints());
            //Debug.Log(gameObject.GetComponent<Team>().GetTeam());
            //Debug.Log(other.gameObject.GetComponent<Team>().GetTeam());
            if (other.gameObject.CompareTag("Base"))
            {
                //if your base set IsInBase, and if !IsActive, sets IsActive to true, if enemy base and IsActive is true, get 10000 points
                if (other.gameObject.GetComponent <Team>().GetTeam() == gameObject.GetComponent<Team>().GetTeam())
                {
                    if (gameObject.GetComponent<Health>().IsInBase == false)
                    {
                        gameObject.GetComponent<Health>().IsInBase = true;
                        if (gameObject.GetComponent<Health>().IsActive == false)
                        {
                            gameObject.GetComponent<Health>().IsActive = true;
                        }
                        // set IsInBase
                        gameObject.GetComponent<Health>().IsInBase = true;
                    }
                    Debug.Log(gameObject.GetComponent<Health>().IsActive);
                }
                else if (other.gameObject.GetComponent<Team>().GetTeam() != gameObject.GetComponent<Team>().GetTeam())
                {
                    Debug.Log(HasReachedEnemyBase);
                    Debug.Log(gameObject.GetComponent<Health>().IsActive);
                    if (gameObject.GetComponent<Health>().IsActive == true && HasReachedEnemyBase == false)
                    {
                        Debug.Log("hoy");
                        gameObject.GetComponent<Health>().IncreasePoints(10000, other.gameObject);
                        HasReachedEnemyBase = true;
                        gameObject.GetComponent<Health>().IsActive = false;
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
                    gameObject.GetComponent<Health>().IsInBase = false;
                    Debug.Log("Hey");
                }
            }
        }
    }
}

