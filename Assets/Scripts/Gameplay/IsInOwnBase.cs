using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osmose.Gameplay
{
    public class IsInOwnBase : MonoBehaviour
    {
        CapsuleCollider playerHitbox;
        PlayerMotor motor;

        //public bool IsInOwnBase { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            playerHitbox = GetComponent<CapsuleCollider>();
            motor = GetComponent<PlayerMotor>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("BaseHitbox"))
            {
                if (other.gameObject.GetComponent<Team>().GetTeam() == gameObject.GetComponent<Team>().GetTeam())
                {
                    //IsInOwnBase = true;
                    Debug.Log("IsInOwnBase");
                    motor.playBase();
                    gameObject.GetComponent<Health>().IsInBase = true;
                    gameObject.GetComponent<Health>().CanMatch = true;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("BaseHitbox"))
            {
                if (other.gameObject.GetComponent<Team>().GetTeam() == gameObject.GetComponent<Team>().GetTeam())
                {
                    //IsInOwnBase = false;
                    Debug.Log("IsNotInOwnBase");
                    gameObject.GetComponent<Health>().IsInBase = false;
                }
            }
        }
    }
}