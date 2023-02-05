using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class Associate : MonoBehaviour
    {
        // Start is called before the first frame update
        GameObject otherPlayer;
        GameObject player;
        public bool isAssociated;
        void Start()
        {
            isAssociated = false;
            player = transform.parent.gameObject;
            otherPlayer = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (isAssociated)
            {
                // computes distance between the two players
                float distance = Vector3.Distance(transform.position, otherPlayer.transform.position);
                // if the distance is too big, the players are not associated anymore
                if (distance > 2.5f)
                {
                    isAssociated = false;
                    Debug.Log(isAssociated);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!isAssociated)
            {
                if (other.gameObject.CompareTag("AttackHitbox"))
                {
                    otherPlayer = other.transform.parent.gameObject;
                    if (otherPlayer.GetComponent<Team>().GetTeam() == player.GetComponent<Team>().GetTeam())
                    {
                        isAssociated = true;
                    }
                    else
                    {
                        otherPlayer = null;
                    }
                    //Debug.Log(isAssociated);
                    //Debug.Log(otherPlayer);
                }
            }
        }

        void OnChangePoints()
        {
            if (isAssociated)
            {
                // if the player gains or loses points while associated, both players gain or lose points half those points


            }
        }

        // GetAssociatedPlayer method returns otherPlayer
        public GameObject GetAssociatedPlayer()
        {
            return otherPlayer;
        }
    }
}