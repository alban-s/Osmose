using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Osmose.Gameplay
{
    public class CollisionWithBase : MonoBehaviour
    {
        //public bool HasReachedEnemyBase { get; set; }
        public bool isActive;
        private GameObject player;
        PlayerMotor motor;
        // Start is called before the first frame update
        void Start()
        {
            //HasReachedEnemyBase = false;
            isActive = false;
            // get the parent of the object
            player = transform.parent.gameObject;
            motor = transform.parent.gameObject.GetComponent<PlayerMotor>();
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
            if (isActive)
            {
                if (other.gameObject.CompareTag("Base"))
                {
                    /*//if your base set IsInBase, and if !CanMatch, sets CanMatch to true, if enemy base and canMatch is true, get 10000 points
                    if (other.gameObject.GetComponent<Team>().GetTeam() == player.GetComponent<Team>().GetTeam())
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
                        //motor.playPoints();
                    }
                    else*/
                    Debug.Log(other.gameObject.GetComponent<Team>().GetTeam());
                    Debug.Log(player.GetComponent<Team>().GetTeam());
                    if (other.gameObject.GetComponent<Team>().GetTeam() != player.GetComponent<Team>().GetTeam())
                    {
                        Debug.Log(player.GetComponent<Health>().CanMatch);
                        if (player.GetComponent<Health>().CanMatch == true)// && HasReachedEnemyBase == false)
                        {
                            Debug.Log("hoy");
                            motor.playEnnemyBase();
                            player.GetComponent<Health>().IncreasePoints(10000, other.gameObject);
                            //HasReachedEnemyBase = true;
                            player.GetComponent<Health>().CanMatch = false;
                        }
                    }
                }
            }
        }
        /*
        //When Player is not touching the base, set IsInBase to false
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Base"))
            {
                if (other.gameObject.GetComponent<Team>().GetTeam() == player.GetComponent<Team>().GetTeam())
                {
                    player.GetComponent<Health>().IsInBase = false;
                    Debug.Log("Hey");
                }
            }
        }*/
    }
}