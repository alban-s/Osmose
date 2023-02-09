using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

namespace Osmose.Gameplay
{
    public class AttackHitboxHandler : NetworkBehaviour
    {

        private CapsuleCollider AttackHitbox;
        private GameObject HitboxObject;
        
        [SyncVar]
        private bool isActivated;
        private int timer;
        // Start is called before the first frame update

        public void OnAttack()
        {
            SetActivated(true);
            HitboxObject.GetComponent<CollisionWithPlayer>().isActive = true;
            timer = 83;
        }

        public void OnAttackBase()
        {
            SetActivated(true);
            HitboxObject.GetComponent<CollisionWithBase>().isActive = true;
            timer = 83;
        }

        public void OnOpenChest()
        {
            SetActivated(true);
            HitboxObject.GetComponent<CollisionWithChest>().isActive = true;
            timer = 30;
        }

        public void OnAssociate()
        {
            SetActivated(true);
            timer = 150;
        }

        [Command(requiresAuthority = false)]

        public void SetActivated(bool active)
        {
            isActivated = active;
        }

        [Command(requiresAuthority = false)]

        public void CmdSetAssociateActivated(bool active)
        {
            RpcUpdateAssociated(active);
        }

        [ClientRpc]
        public void RpcUpdateAssociated(bool asted){
            GameObject ac = gameObject.transform.Find("attackCollider").gameObject;
            ac.GetComponent<Associate>().SetAssociated(asted);
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
            HitboxObject =  transform.Find("attackCollider").gameObject;
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
                    SetActivated(false);
                    HitboxObject.GetComponent<CollisionWithPlayer>().isActive = false;
                    HitboxObject.GetComponent<CollisionWithChest>().isActive = false;
                    HitboxObject.GetComponent<CollisionWithBase>().isActive = false;
                }
            }
            AttackHitbox.enabled = isActivated;
        }
    }
}