using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Osmose.Game {
    public class Associate : MonoBehaviour
    {
        // Start is called before the first frame update
        public ushort points_fusionned;
        public bool islink;
        public int linked_player_id;
        public GameObject [] player_list;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!gameObject.GetComponent<Health>().IsActive)
            {
                SeDelier();
            }

        }
        void SeLier(GameObject joueur2)
        {
            islink = true;
            points_fusionned = (ushort) (gameObject.GetComponent<Health>().CurrentPoints + joueur2.GetComponent<Health>().CurrentPoints);
        }

        void SeDelier()
        {
            if (!islink)
            {
                return;
            }
            islink = false;
            foreach (GameObject player in player_list)
            {
                if (player.GetComponent<Associate>().linked_player_id == this.GetComponent<Associate>().linked_player_id)
                {
                    player.GetComponent<Associate>().linked_player_id = -1;
                    player.GetComponent<Associate>().SeDelier();
                }
            }

        }
    }
}
