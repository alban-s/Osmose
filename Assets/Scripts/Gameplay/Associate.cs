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
        [SyncVar]
        public bool isAssociated;
        void Start()
        {
            SetAssiociated(false);
            player = gameObject;
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
                    SetAssiociated(false);
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
                    Debug.LogError(otherPlayer.transform.name);
                    GameObject this_player = gameObject.transform.parent.gameObject;
                    Debug.LogError(this_player.transform.name);
                    if (otherPlayer.GetComponent<Team>().GetTeam() == this_player.GetComponent<Team>().GetTeam())
                    {
                        SetAssiociated(true);
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

        [Command(requiresAuthority = false)]
        private void SetAssiociated(bool associate)
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