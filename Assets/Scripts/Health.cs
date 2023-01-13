using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Osmose.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Starting amount of points")] public ushort StartPoints = 100;
        [Tooltip("Current amount of points")] public ushort CurrentPoints { get; set; }
        public UnityAction<ushort, GameObject> OnLoseMatch;
        public UnityAction<ushort, GameObject> OnWinMatch;
        public UnityAction OnDie;
        public bool Invincible { get; set; }
        public bool CanMatch { get; set; }
        public bool IsInBase  { get; set; }
        public ushort GetPoints() => CurrentPoints;
        private TeamColour team;
        bool m_IsDead;
        public bool islink;

        void Start()
        {
            CurrentPoints = StartPoints;
            // num_equipe = Random.Range(1,3);
            team = gameObject.GetComponent<Team>().GetTeam(); 
            ChoixEquipe();
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
                OnDie?.Invoke();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ChoixEquipe(){
            if(team == TeamColour.Red){
                GetComponent<Renderer>().material.color = Color.red;

            }
            if(team == TeamColour.Blue) {
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }

       
    }
}

