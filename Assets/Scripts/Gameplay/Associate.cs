using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            otherPlayer = other.transform.parent.gameObject;
            isAssociated = true;
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
    GameObject GetAssociatePlayer()
    {
        return otherPlayer;
    }
}