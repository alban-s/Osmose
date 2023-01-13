using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public bool CanMatch { get; set; }
        public bool IsInBase { get; set; }
        public float GetPoints() => CurrentPoints;

        bool m_IsDead;

        void Start()
        {
            CurrentPoints = StartPoints;
            CanMatch = true;
        }


        public void IncreasePoints(ushort increaseAmount, GameObject pointsSource)
        {
            ushort tempPoints = CurrentPoints;
            CurrentPoints += increaseAmount;

            //call OnWinMatch action
            ushort trueWinAmount = (ushort) (CurrentPoints - tempPoints);
            if (trueWinAmount > 0)
            {
                OnWinMatch?.Invoke(trueWinAmount, pointsSource);
                Debug.Log("+" + trueWinAmount);
            }
        }


        public void DecreasePoints(ushort damage, GameObject damageSource)
        {
            if (Invincible)
                return;

            ushort tempPoints = CurrentPoints;
            CurrentPoints -= damage;

            //call OnLoseMatch action
            ushort trueLoseAmount = (ushort) (tempPoints - CurrentPoints);
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

