using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithPlayer : MonoBehaviour
    {
        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("start");
            // get the parent of the object
            player = transform.parent.gameObject;
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
                    if (otherPlayer.GetComponent<Health>().CanMatch && !gameObject.GetComponent<Team>().IsSameTeam(other.gameObject.GetComponent<Team>().GetTeam()))
                    {
                        //if player IsInBase, wins match
                        if (player.GetComponent<Health>().IsInBase == true)
                        {
                            player.GetComponent<Health>().IncreasePoints(500, other.gameObject);
                            otherPlayer.GetComponent<Health>().DecreasePoints(500, gameObject);
                        }
                        // if other player IsInBase, loses match
                        else if (otherPlayer.GetComponent<Health>().IsInBase == true)
                        {
                            otherPlayer.GetComponent<Health>().IncreasePoints(500, gameObject);
                            player.GetComponent<Health>().DecreasePoints(500, other.gameObject);
                        }
                        // if neither player is in base, compare points
                        else if (otherPlayer.GetComponent<Health>().CurrentPoints > gameObject.GetComponent<Health>().CurrentPoints)
                        {
                            //winning player steals 500 points from losing player
                            otherPlayer.GetComponent<Health>().IncreasePoints(500, gameObject);
                            player.GetComponent<Health>().DecreasePoints(500, other.gameObject);
                            Debug.Log("You Lose : " + player.GetComponent<Health>().GetPoints());
                        }
                        else if (otherPlayer.GetComponent<Health>().CurrentPoints < gameObject.GetComponent<Health>().CurrentPoints)
                        {
                            //winning player steals 500 points from losing player
                            otherPlayer.GetComponent<Health>().DecreasePoints(500, gameObject);
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
                        if (!player.GetComponent<Health>().isTest) gameObject.GetComponent<Health>().CanMatch = false;
                    }
                }
            }
        }
        //When colliding with another player, checks the difference of points between them
        void OnCollisionEnter(Collision other)
        {
            HitPlayer(other.gameObject);
        }
    }
}