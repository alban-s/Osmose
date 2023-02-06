using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Osmose.Game;

namespace Osmose.Gameplay
{
    public class CollisionWithChest : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("Amount of health to heal on pickup")]
        
        private ushort PointsAmount;
        private GameObject player;
        public bool isActive;

        // Start is called before the first frame update
        void Start()
        {

            // get the parent of the object
            player = transform.parent.gameObject;
            isActive = false;
            Debug.Log(player);
            Debug.Log(player.tag);
            Debug.Log(player.GetComponent<Health>().GetPoints());
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            // Debug show tag of other
            if (isActive)
            {
                Debug.Log(other.gameObject.tag);
                if (other.gameObject.CompareTag("Chest"))
                {

                    Debug.Log("Collision with chest");
                    PointsAmount = other.gameObject.GetComponent<ChestController>().GetPoints();
                    Debug.Log(PointsAmount);
                    player.GetComponent<Health>().IncreasePoints(PointsAmount, other.gameObject);
                    other.gameObject.SetActive(false);
                    
                    //other.gameObject.GetComponent<ChestController>().PickedUp();
                }
            }
        }
    }

}