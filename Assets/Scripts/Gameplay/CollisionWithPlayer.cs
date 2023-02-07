using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithPlayer : MonoBehaviour
    {
        GameObject player;
        public bool isActive;
        PlayerMotor motor;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("start");
            isActive = false;
            // get the parent of the object
            player = transform.parent.gameObject;
            motor = transform.parent.gameObject.GetComponent<PlayerMotor>();            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HitPlayer(GameObject other)
        {
            
            if (player.GetComponent<Health>().CanMatch)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    GameObject otherPlayer = other.gameObject;
                    Debug.Log("collision");
                    if (otherPlayer.GetComponent<Health>().CanMatch && !player.GetComponent<Team>().IsSameTeam(other.gameObject.GetComponent<Team>().GetTeam()))
                    {
                        motor.playAttack();
                        //if player IsInBase, wins match
                        if (player.GetComponent<Health>().IsInBase == true)
                        {
                            player.GetComponent<Health>().IncreasePoints(10000, other.gameObject);
                            otherPlayer.GetComponent<Health>().DecreasePoints(10000, player);
                        }
                        // if other player IsInBase, loses match
                        else if (otherPlayer.GetComponent<Health>().IsInBase == true)
                        {
                            otherPlayer.GetComponent<Health>().IncreasePoints(10000, player);
                            player.GetComponent<Health>().DecreasePoints(10000, other.gameObject);
                        }
                        // if neither player is in base, compare points
                        else if (otherPlayer.GetComponent<Health>().GetPoints() > player.GetComponent<Health>().GetPoints())
                        {
                            //winning player steals 500 points from losing player
                            otherPlayer.GetComponent<Health>().IncreasePoints(500, player);
                            player.GetComponent<Health>().DecreasePoints(500, other.gameObject);
                            Debug.Log("You Lose : " + player.GetComponent<Health>().GetPoints());
                        }
                        else if (otherPlayer.GetComponent<Health>().GetPoints() < player.GetComponent<Health>().GetPoints())
                        {
                            //winning player steals 500 points from losing player
                            otherPlayer.GetComponent<Health>().DecreasePoints(500, player);
                            player.GetComponent<Health>().IncreasePoints(500, other.gameObject);
                            Debug.Log("You Win : " + player.GetComponent<Health>().GetPoints());
                            // other.gameObject.GetComponent<Health>().winText.text = "You Lose!";
                        }
                        else
                        {
                            //other.gameObject.GetComponent<Health>().winText.text = "Draw!";
                            //gameObject.GetComponent<Health>().winText.text = "Draw!";
                        }
                        //set both players to !CanMatch
                        if (!otherPlayer.GetComponent<Health>().isTest) other.gameObject.GetComponent<Health>().CanMatch = false;
                        if (!player.GetComponent<Health>().isTest) player.GetComponent<Health>().CanMatch = false;
                    }
                    else if (!otherPlayer.GetComponent<Health>().CanMatch && !player.GetComponent<Team>().IsSameTeam(other.gameObject.GetComponent<Team>().GetTeam()))
                    {
                        motor.playAttackNull();
                        motor.cantMoveAttackNull();
                        otherPlayer.GetComponent<PlayerMotor>().cantMoveAttackNull();
                    }
                }
            }
            else if(!player.GetComponent<Health>().CanMatch)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (!player.GetComponent<Team>().IsSameTeam(other.gameObject.GetComponent<Team>().GetTeam()))
                    {
                        motor.playAttackNull();
                        motor.cantMoveAttackNull();
                        other.gameObject.GetComponent<PlayerMotor>().cantMoveAttackNull();
                    }
                }
            }
        }
        //When colliding with another player, checks the difference of points between them
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("collision");
            if (isActive) HitPlayer(other.gameObject);
        }
    }
}