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
        PlayerMotor motor;
        float tim = 0.0f;
        GameObject cylinderOn;
        GameObject cylinderOff;
        bool collisionChest = false;

        // Start is called before the first frame update
        void Start()
        {
            // get the parent of the object
            player = transform.parent.gameObject;
            isActive = false;
            Debug.Log(player);
            Debug.Log(player.tag);
            Debug.Log(player.GetComponent<Health>().GetPoints());
            motor = transform.parent.gameObject.GetComponent<PlayerMotor>();
        }

        // Update is called once per frame
        void Update()
        {            
            //tim -= 1.0f;
            tim -= Time.deltaTime;
            if(tim < 0.0f && collisionChest) 
            {
                collisionChest = false;
                cylinderOn.SetActive(false);
                cylinderOff.SetActive(true);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // Debug show tag of other
            if (isActive)
            {
                Debug.Log(other.gameObject.tag);
                if (other.gameObject.CompareTag("Chest"))
                {
                    tim = 1.7f;
                    collisionChest = true;
                    Debug.Log("Collision with chest");
                    motor.playChest();
                    PointsAmount = other.gameObject.GetComponent<ChestController>().GetPoints();
                    Debug.Log(PointsAmount);
                    player.GetComponent<Health>().IncreasePoints(PointsAmount, other.gameObject);
                    
                    //other.gameObject.SetActive(false);
                    other.gameObject.GetComponent<MeshCollider>().enabled = false;
                    //other.gameObject.transform.Find("cylinderOn").gameObject.SetActive(false);
                    //other.gameObject.transform.Find("cylinderOff").gameObject.SetActive(true);

                    cylinderOn = other.gameObject.transform.Find("cylinderOn").gameObject;
                    cylinderOff = other.gameObject.transform.Find("cylinderOff").gameObject;
                    
                    //other.gameObject.GetComponent<ChestController>().PickedUp();
                }
                
            }
        }
    }

}