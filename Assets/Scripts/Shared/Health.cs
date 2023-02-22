using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

namespace Osmose.Gameplay
{
    public class Health : NetworkBehaviour
    {

        [Tooltip("Starting amount of points")] public int StartPoints;
        [Tooltip("Current amount of points")] public int CurrentPoints;

        [SyncVar]
        [SerializeField] public bool CanMatch;
        [SyncVar]
        bool m_IsDead;
        public bool isTest;
        public UnityAction<int, GameObject> OnLoseMatch;
        public UnityAction<int, GameObject> OnWinMatch;
        public UnityAction OnDie;
        private GameObject GameManager;

        private Animator animator;
        [SerializeField]
        private GameObject laser;
        private GameObject instance;

        public bool Invincible { get; set; }

        public bool IsInBase { get; set; }
        //public int GetPoints() => (int)((!gameObject.GetComponent<Associate>().isAssociated) ? CurrentPoints :
        //    gameObject.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().CurrentPoints + CurrentPoints);

        public int GetPoints() => (int)((!transform.Find("attackCollider").gameObject.GetComponent<Associate>().isAssociated)? CurrentPoints :
            transform.Find("attackCollider").gameObject.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().CurrentPoints + CurrentPoints);
        void Start()
        {
            StartPoints = 0;
            animator = GetComponentInChildren<Animator>();
            GameManager = GameObject.Find("GameManager");
            CurrentPoints = StartPoints;
            CanMatch = true;
            IsInBase = false;
            /*if (GetComponent<Team>().team == TeamColour.Red)
            {
                OnWinMatch += GameManager.GetComponent<Score>().IncreaseScoreRed;
                OnLoseMatch += GameManager.GetComponent<Score>().DecreaseScoreRed;
            }
            if (GetComponent<Team>().team == TeamColour.Blue)
            {
                OnWinMatch += GameManager.GetComponent<Score>().IncreaseScoreBlue;
                OnLoseMatch += GameManager.GetComponent<Score>().DecreaseScoreBlue;
            }*/
        }

        [Command(requiresAuthority = false)]
        void update_score_clients()
        {
            List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            foreach (GameObject player in players)
            {
                int score = player.GetComponent<Health>().CurrentPoints;
                player.GetComponent<Health>().rpc_update_score_client(score);
            }
        }

        [ClientRpc]
        void rpc_update_score_client(int score)
        {
            CurrentPoints = score;
            // Debug.Log("cp : " + GetComponent<Health>().CurrentPoints);
        }

        [Command]
        public void SetStartPoints(int startPoints)
        {
            StartPoints = startPoints;
            CurrentPoints = StartPoints;
            update_score_clients();
        }


        public void IncreasePoints(int increaseAmount, GameObject pointsSource, bool associated = false){
            CmdIncreasePoints(increaseAmount, pointsSource, associated);
        }
        [Command (requiresAuthority = false)]
        public void CmdIncreasePoints(int increaseAmount, GameObject pointsSource, bool associated)
        {
            int tempPoints = CurrentPoints;

            // if the player is associated with another, both gain half the points otherwise the player gains all the points
            GameObject attackHitbox = transform.Find("attackCollider").gameObject;
            if (attackHitbox.GetComponent<Associate>().GetAssociatedPlayer() != null && associated)
            {
                CurrentPoints += (int)(increaseAmount / 2);
                attackHitbox.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().IncreasePoints((int)(increaseAmount / 2), pointsSource, true);
            }
            else
            {
                CurrentPoints += increaseAmount;
                Debug.Log(gameObject.transform.name + " a gagné 500 se retrouve a " + CurrentPoints);
            }
            update_score_clients();

            //CurrentPoints += increaseAmount;

            //call OnWinMatch action
            int trueWinAmount = (int)(CurrentPoints - tempPoints);
            if (trueWinAmount > 0)
            {
                OnWinMatch?.Invoke(trueWinAmount, pointsSource);
                // Debug.Log("+" + trueWinAmount + "->" + CurrentPoints);
            }
        }

        public void DecreasePoints(int damage, GameObject damageSource, bool associated = false){
            CmdDecreasePoints(damage, damageSource, associated);
        }
        [Command(requiresAuthority = false)]
        public void CmdDecreasePoints(int damage, GameObject damageSource, bool associated)
        {
            if (Invincible)
                return;

            int tempPoints = CurrentPoints;
            GameObject attackHitbox = transform.Find("attackCollider").gameObject;
            if (attackHitbox.GetComponent<Associate>().GetAssociatedPlayer() != null && associated)
            {
                CurrentPoints = (int)((damage/2>=CurrentPoints)? 0 : (int)(CurrentPoints - damage / 2));
                attackHitbox.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().IncreasePoints((int)(damage / 2), damageSource, true);
            }
            else
            {
                CurrentPoints = (int)((damage>=CurrentPoints)? 0 : (int) (CurrentPoints - damage));
                Debug.Log(gameObject.transform.name + " a perdu " + damage + " se retrouve a " + CurrentPoints);
            }
            update_score_clients();
            //CurrentPoints -= damage;

            //call OnLoseMatch action
            int trueLoseAmount = (int)(tempPoints - CurrentPoints);
            if (trueLoseAmount > 0)
            {
                OnLoseMatch?.Invoke(trueLoseAmount, damageSource);
                // Debug.Log("-" + trueLoseAmount);
            }

            RpcHandleDeath();
        }

        //DEPRECATED / NEVER USED
        public void Kill()
        {
            CurrentPoints = 0;
            update_score_clients();
            //call OnLoseMatch action
            OnLoseMatch?.Invoke(CurrentPoints, null);

            //HandleDeath();
        }

        [ClientRpc]
        void RpcHandleDeath(){
            if (m_IsDead)
                return;

            if (CurrentPoints <= 0)
            {
                m_IsDead = true;
                dead();
                StartCoroutine(wait());
                //gameObject.SetActive(false);
                OnDie?.Invoke();
            }
        }

        void dead()
        {
            animator.Play("Base Layer.Death");
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Quaternion rotation = new Quaternion((float)-0.697247863, (float)-0.0185691323, (float)-0.00647618202, (float)0.716560304);
            instance = Instantiate(laser);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.SetActive(true);
            gameObject.GetComponent<PlayerMotor>().enabled = false;
            gameObject.GetComponent<PlayerController>().enabled = false;
        }

        IEnumerator wait()
        {
            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);
            // laser.SetActive(false);
            instance.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

