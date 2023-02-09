using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Osmose.Gameplay
{
    public class Associate : NetworkBehaviour
    {
        // Start is called before the first frame update
        GameObject otherPlayer;
        GameObject player;
        public bool isAssociated;
        void Start()
        {
            SetAssociated(false);
            player = gameObject.transform.parent.gameObject;
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
                    SetAssociated(false);
                    Debug.Log(isAssociated);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("TRIGGER");
            if (!isAssociated)
            {
                Debug.LogError("PAS encore ASSOCIER");
                Debug.LogError(other.gameObject.transform.name);
                Debug.LogError(other.gameObject.tag);
                if (other.gameObject.CompareTag("AttackHitbox"))
                {
                    Debug.LogError("ASSOCIATION");
                    otherPlayer = other.transform.parent.gameObject;
                    if (otherPlayer.GetComponent<Team>().GetTeam() == player.GetComponent<Team>().GetTeam())
                    {
                        player.GetComponent<AttackHitboxHandler>().CmdSetAssociateActivated(true);
                    }
                    else
                    {
                        otherPlayer = null;
                    }
                    Debug.Log(isAssociated);
                    Debug.Log(otherPlayer);
                }
            }
        }
        public void SetAssociated(bool associate)
        {
            isAssociated = associate;
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