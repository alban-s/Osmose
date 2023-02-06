using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Osmose.Gameplay;

namespace Osmose.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Starting amount of points")] public ushort StartPoints = 10000;
        [Tooltip("Current amount of points")] public ushort CurrentPoints { get; set; }

        public bool isTest;
        public UnityAction<ushort, GameObject> OnLoseMatch;
        public UnityAction<ushort, GameObject> OnWinMatch;
        public UnityAction OnDie;

        public bool Invincible { get; set; }
        [SerializeField] public bool CanMatch;  //{ get; set; }
        public bool IsInBase { get; set; }
        public float GetPoints() => (!transform.GetChild(2).gameObject.GetComponent<Associate>().isAssociated)? CurrentPoints :
            transform.GetChild(2).gameObject.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().CurrentPoints + CurrentPoints;

        bool m_IsDead;

        void Start()
        {
            CurrentPoints = StartPoints;
            CanMatch = true;
            IsInBase = false;
        }


        public void IncreasePoints(ushort increaseAmount, GameObject pointsSource, bool associated = false)
        {
            ushort tempPoints = CurrentPoints;
            // if the player is associated with another, both gain half the points otherwise the player gains all the points
            GameObject attackHitbox = transform.GetChild(2).gameObject;
            if (attackHitbox.GetComponent<Associate>().GetAssociatedPlayer() != null && !associated)
            {
                CurrentPoints += (ushort)(increaseAmount / 2);
                attackHitbox.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().IncreasePoints((ushort)(increaseAmount / 2), pointsSource, true);
            }
            else
            {
                CurrentPoints += increaseAmount;
            }

            //CurrentPoints += increaseAmount;

            //call OnWinMatch action
            ushort trueWinAmount = (ushort)(CurrentPoints - tempPoints);
            if (trueWinAmount > 0)
            {
                OnWinMatch?.Invoke(trueWinAmount, pointsSource);
                Debug.Log("+" + trueWinAmount + "->" + CurrentPoints);
            }
        }
        public void DecreasePoints(ushort damage, GameObject damageSource, bool associated = false)
        {
            if (Invincible)
                return;

            ushort tempPoints = CurrentPoints;
            GameObject attackHitbox = transform.GetChild(2).gameObject;
            if (attackHitbox.GetComponent<Associate>().GetAssociatedPlayer() != null && !associated)
            {
                CurrentPoints -= (ushort)(damage / 2);
                attackHitbox.GetComponent<Associate>().GetAssociatedPlayer().GetComponent<Health>().IncreasePoints((ushort)(damage / 2), damageSource, true);
            }
            else
            {
                CurrentPoints -= damage;
            }
            //CurrentPoints -= damage;

            //call OnLoseMatch action
            ushort trueLoseAmount = (ushort)(tempPoints - CurrentPoints);
            if (trueLoseAmount > 0)
            {
                OnLoseMatch?.Invoke(trueLoseAmount, damageSource);
                Debug.Log("-" + trueLoseAmount);
            }

            HandleDeath();
        }


        public void Kill()
        {
            CurrentPoints = 0;
            //call OnLoseMatch action
            OnLoseMatch?.Invoke(CurrentPoints, null);
            
            HandleDeath();
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            if (CurrentPoints <= 0)
            {
                m_IsDead = true;
                gameObject.SetActive(false);
                OnDie?.Invoke();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

