using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithBase : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //When Player is touching the base, set IsInBase to true, if they are !CanMatch, sets them to CanMatch
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Base"))
            {
                if (gameObject.GetComponent<Health>().CanMatch == false)
                {
                    gameObject.GetComponent<Health>().CanMatch = true;
                }
                gameObject.GetComponent<Health>().IsInBase = true;
            }
        }

        //When Player is not touching the base, set IsInBase to false
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Base"))
            {
                gameObject.GetComponent<Health>().IsInBase = false;
            }
        }
    }
}

