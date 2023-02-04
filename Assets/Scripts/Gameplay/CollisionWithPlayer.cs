using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("start");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HitPlayer(GameObject other)
        {
            
            if (gameObject.GetComponent<Health>().IsActive)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("collision");
                    if (other.gameObject.GetComponent<Health>().IsActive && !gameObject.GetComponent<Team>().IsSameTeam(other.gameObject.GetComponent<Team>().GetTeam()))
                    {
                        //if player IsInBase, wins match
                        if (gameObject.GetComponent<Health>().IsInBase == true)
                        {
                            gameObject.GetComponent<Health>().IncreasePoints(500, other.gameObject);
                            other.gameObject.GetComponent<Health>().DecreasePoints(500, gameObject);
                        }
                        // if other player IsInBase, loses match
                        else if (other.gameObject.GetComponent<Health>().IsInBase == true)
                        {
                            other.gameObject.GetComponent<Health>().IncreasePoints(500, gameObject);
                            gameObject.GetComponent<Health>().DecreasePoints(500, other.gameObject);
                        }
                        // if neither player is in base, compare points
                        else if (other.gameObject.GetComponent<Health>().CurrentPoints > gameObject.GetComponent<Health>().CurrentPoints)
                        {
                            //winning player steals 500 points from losing player
                            other.gameObject.GetComponent<Health>().IncreasePoints(500, gameObject);
                            gameObject.GetComponent<Health>().DecreasePoints(500, other.gameObject);
                            Debug.Log("You Lose : " + gameObject.GetComponent<Health>().GetPoints());
                        }
                        else if (other.gameObject.GetComponent<Health>().CurrentPoints < gameObject.GetComponent<Health>().CurrentPoints)
                        {
                            //winning player steals 500 points from losing player
                            other.gameObject.GetComponent<Health>().DecreasePoints(500, gameObject);
                            gameObject.GetComponent<Health>().IncreasePoints(500, other.gameObject);
                            Debug.Log("You Win : " + gameObject.GetComponent<Health>().GetPoints());
                            // other.gameObject.GetComponent<Health>().winText.text = "You Lose!";
                        }
                        else
                        {
                            //other.gameObject.GetComponent<Health>().winText.text = "Draw!";
                            //gameObject.GetComponent<Health>().winText.text = "Draw!";
                        }
                        //set both players to !IsActive
                        if (!other.gameObject.GetComponent<Health>().isTest) other.gameObject.GetComponent<Health>().IsActive = false;
                        if (!gameObject.GetComponent<Health>().isTest) gameObject.GetComponent<Health>().IsActive = false;
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