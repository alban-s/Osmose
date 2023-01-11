using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Osmose.Game;

namespace Osmose.Gamplay
{
    public class CollisionWithChest : MonoBehaviour
    {
        [Header("Parameters")][Tooltip("Amount of health to heal on pickup")]
        public ushort PointsAmount;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Chest"))
            {
                gameObject.GetComponent<Health>().IncreasePoints(PointsAmount, other.gameObject);
                other.gameObject.GetComponent<ChestController>().PickedUp();
            }
        }
    }

}
