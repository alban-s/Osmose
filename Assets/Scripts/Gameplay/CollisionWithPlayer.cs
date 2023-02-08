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
            if (other.gameObject.CompareTag("Player"))
            {
                GameObject otherPlayer = other.gameObject;
                if (player.GetComponent<Health>().CanMatch)
                {
                    Debug.Log("collision");
                    if (otherPlayer.GetComponent<Health>().CanMatch && !player.GetComponent<Team>().IsSameTeam(otherPlayer.GetComponent<Team>().GetTeam()))
                    {
                        motor.playAttack();
                        //if player IsInBase, wins match
                        if (player.GetComponent<Health>().IsInBase == true)
                        {
                            player.GetComponent<Health>().IncreasePoints(10000, otherPlayer);
                            otherPlayer.GetComponent<Health>().DecreasePoints(10000, player);
                        }
                        // if other player IsInBase, loses match
                        else if (otherPlayer.GetComponent<Health>().IsInBase == true)
                        {
                            otherPlayer.GetComponent<Health>().IncreasePoints(10000, player);
                            player.GetComponent<Health>().DecreasePoints(10000, otherPlayer);
                        }
                        // if neither player is in base, compare points
                        else if (otherPlayer.GetComponent<Health>().GetPoints() > player.GetComponent<Health>().GetPoints())
                        {
                            //winning player steals 500 points from losing player
                            otherPlayer.GetComponent<Health>().IncreasePoints(500, player);
                            player.GetComponent<Health>().DecreasePoints(500, otherPlayer);
                            Debug.Log("You Lose : " + player.GetComponent<Health>().GetPoints());
                        }
                        else if (otherPlayer.GetComponent<Health>().GetPoints() < player.GetComponent<Health>().GetPoints())
                        {
                            //winning player steals 500 points from losing player
                            otherPlayer.GetComponent<Health>().DecreasePoints(500, player);
                            player.GetComponent<Health>().IncreasePoints(500, otherPlayer);
                            Debug.Log("You Win : " + player.GetComponent<Health>().GetPoints());
                            // otherPlayer.GetComponent<Health>().winText.text = "You Lose!";
                        }
                        else
                        {
                            //otherPlayer.GetComponent<Health>().winText.text = "Draw!";
                            //gameObject.GetComponent<Health>().winText.text = "Draw!";
                        }
                        //set both players to !CanMatch
                        if (!otherPlayer.GetComponent<Health>().isTest) otherPlayer.GetComponent<Health>().CanMatch = false;
                        if (!player.GetComponent<Health>().isTest) player.GetComponent<Health>().CanMatch = false;
                    }
                    else if (!otherPlayer.GetComponent<Health>().CanMatch && !player.GetComponent<Team>().IsSameTeam(otherPlayer.GetComponent<Team>().GetTeam()))
                    {
                        motor.playAttackNull();
                        motor.cantMoveAttackNull();
                        otherPlayer.GetComponent<PlayerMotor>().cantMoveAttackNull();
                    }
                }
                else if(!player.GetComponent<Health>().CanMatch)
                {
                    if (!player.GetComponent<Team>().IsSameTeam(otherPlayer.GetComponent<Team>().GetTeam()))
                    {
                        motor.playAttackNull();
                        motor.cantMoveAttackNull();
                        otherPlayer.GetComponent<PlayerMotor>().cantMoveAttackNull();
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