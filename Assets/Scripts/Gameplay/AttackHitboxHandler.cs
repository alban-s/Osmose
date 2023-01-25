using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class AttackHitboxHandler : MonoBehaviour
    {

        public Collider AttackHitbox;

        private bool isActivated;
        // Start is called before the first frame update

        private void OnEnable()
        {
            isActivated = true;
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && isActivated)
            {
                gameObject.GetComponent<CollisionWithPlayer>().HitPlayer(other.gameObject);
                //isActivated = false;
            }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}