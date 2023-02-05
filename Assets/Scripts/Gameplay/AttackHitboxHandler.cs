using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class AttackHitboxHandler : MonoBehaviour
    {

        private CapsuleCollider AttackHitbox;
        private GameObject HitboxObject;
        
        private bool isActivated;
        private int timer;
        // Start is called before the first frame update

        public void OnAttack()
        {
            isActivated = true;
            HitboxObject.GetComponent<CollisionWithPlayer>().isActive = true;
            timer = 83;
        }

        public void OnAttackBase()
        {
            isActivated = true;
            HitboxObject.GetComponent<CollisionWithBase>().isActive = true;
            timer = 83;
        }

        public void OnOpenChest()
        {
            isActivated = true;
            HitboxObject.GetComponent<CollisionWithChest>().isActive = true;
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
            HitboxObject = transform.GetChild(2).gameObject;
            AttackHitbox = HitboxObject.GetComponent<CapsuleCollider>();
            //AttackHitbox = transform.GetChild(2).gameObject.GetComponent<CapsuleCollider>();
            
            Physics.IgnoreCollision(AttackHitbox, gameObject.GetComponent<CapsuleCollider>());
            Physics.IgnoreCollision(AttackHitbox, gameObject.GetComponent<CharacterController>());
            // Debug
            Debug.Log(AttackHitbox.enabled);
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
                    HitboxObject.GetComponent<CollisionWithPlayer>().isActive = false;
                    HitboxObject.GetComponent<CollisionWithChest>().isActive = false;
                    HitboxObject.GetComponent<CollisionWithBase>().isActive = false;
                }
            }
            AttackHitbox.enabled = isActivated;
        }
    }
}