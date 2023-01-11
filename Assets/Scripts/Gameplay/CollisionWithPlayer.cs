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

        }

        // Update is called once per frame
        void Update()
        {

        }

        //When colliding with another player, checks the difference of points between them
        void onTriggerEnter(Collider other)
        {
            if (gameObject.GetComponent<Health>().CanMatch)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.gameObject.GetComponent<Health>().CanMatch)
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
                        if (other.gameObject.GetComponent<Health>().CurrentPoints > gameObject.GetComponent<Health>().CurrentPoints)
                        {
                            //winning player steals 500 points from losing player
                            other.gameObject.GetComponent<Health>().IncreasePoints(500, gameObject);
                            gameObject.GetComponent<Health>().DecreasePoints(500, other.gameObject);
                            //gameObject.GetComponent<Health>().winText.text = "You Lose!";
                        }
                        else if (other.gameObject.GetComponent<Health>().CurrentPoints < gameObject.GetComponent<Health>().CurrentPoints)
                        {
                            //winning player steals 500 points from losing player
                            other.gameObject.GetComponent<Health>().DecreasePoints(500, gameObject);
                            gameObject.GetComponent<Health>().IncreasePoints(500, other.gameObject);
                            // other.gameObject.GetComponent<Health>().winText.text = "You Lose!";
                        }
                        else
                        {
                            //other.gameObject.GetComponent<Health>().winText.text = "Draw!";
                            //gameObject.GetComponent<Health>().winText.text = "Draw!";
                        }
                        //set both players to !CanMatch
                        other.gameObject.GetComponent<Health>().CanMatch = false;
                        gameObject.GetComponent<Health>().CanMatch = false;
                    }
                }
            }
        }
    }
}