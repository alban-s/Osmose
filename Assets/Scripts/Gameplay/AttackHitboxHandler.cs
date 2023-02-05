using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class AttackHitboxHandler : MonoBehaviour
    {

        private Collider AttackHitbox;

        private bool isActivated;
        private int timer;
        // Start is called before the first frame update

        public void OnAttack()
        {
            isActivated = true;
            timer = 83;
        }

        public void OnOpenChest()
        {
            isActivated = true;
            timer = 30;
        }

        public void OnAssociate()
        {
            isActivated = true;
            timer = 150;
        }


        private void OnTriggerEnter(Collider other)
        {
            /*if (other.gameObject.CompareTag("Player") && isActivated)
            {
                gameObject.GetComponent<CollisionWithPlayer>().HitPlayer(other.gameObject);
                //isActivated = false;
            }*/
        }

        void Start()
        {
            timer = 0;
            CapsuleCollider AttackHitbox = transform.GetChild(2).gameObject.GetComponent<CapsuleCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isActivated)
            {
                timer--;
                if (timer <= 0)
                {
                    isActivated = false;
                }
            }
            AttackHitbox.enabled = isActivated;
        }
    }
}