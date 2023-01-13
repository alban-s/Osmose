using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Osmose.Game {
    public class Associate : MonoBehaviour {
        // Start is called before the first frame update
        public ushort points_fusionned;
        public bool islink;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(!gameObject.GetComponent<Health>().IsActive){
                SeDelier(joueur1,joueur2);
            }

        }
        void SeLier(GameObject joueur1, GameObject joueur2){
            islink = true;
            points_fusionned = joueur1.CurrentPoints + joueur2.CurrentPoints;
        }

        void SeDelier(GameObject joueur1, GameObject joueur){
            islink = false;
        }
    }

}
