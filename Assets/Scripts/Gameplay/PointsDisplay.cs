using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Osmose.Game
{
    public class PointsDisplay : MonoBehaviour
    {

        public Text PointsText;
        // Start is called before the first frame update
        void Start()
        {
            PointsText.text = "Points: " + gameObject.GetComponent<Health>().GetPoints();
        }

        // Update is called once per frame
        void Update()
        {
            PointsText.text = "Points: " + gameObject.GetComponent<Health>().GetPoints();
        }
    } 
}
