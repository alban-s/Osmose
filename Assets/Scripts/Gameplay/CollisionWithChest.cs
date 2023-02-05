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

        // Start is called before the first frame update
        void Start()
        {

            // get the parent of the object
            GameObject player = transform.parent.gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Chest"))
            {
                PointsAmount = other.gameObject.GetComponent<ChestController>().GetPoints();
                player.GetComponent<Health>().IncreasePoints(PointsAmount, other.gameObject);
                other.gameObject.SetActive(false);
                //other.gameObject.GetComponent<ChestController>().PickedUp();
            }
        }
    }

}