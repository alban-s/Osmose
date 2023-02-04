using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using Osmose.Game;

public class Health : NetworkBehaviour
{
    [Tooltip("Starting amount of points")] public ushort StartPoints = 0;

    [SyncVar]
    [Tooltip("Current amount of points")] public ushort CurrentPoints;

    public bool isTest;
    public UnityAction<ushort> OnLoseMatch;
    public UnityAction<ushort> OnWinMatch;
    public UnityAction OnDie;
    private GameObject GameManager;

    public bool Invincible { get; set; }
    public bool IsActive { get; set; }
    public bool IsInBase { get; set; }
    public ushort GetPoints() => CurrentPoints;

    public int player_id;
    bool m_IsDead;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        CurrentPoints = StartPoints;
        IsActive = true;
        if (GetComponent<Team>().team == TeamColour.Red)
        {
            OnWinMatch += GameManager.GetComponent<Score>().IncreaseScoreRed;
            OnLoseMatch += GameManager.GetComponent<Score>().DecreaseScoreRed;
        }
        if (GetComponent<Team>().team == TeamColour.Blue)
        {
            OnWinMatch += GameManager.GetComponent<Score>().IncreaseScoreBlue;
            OnLoseMatch += GameManager.GetComponent<Score>().DecreaseScoreBlue;
        }
    }

    public void SetStartPoints(ushort startPoints)
    {
        StartPoints = startPoints;
    }

    [Command]
    public void IncreasePoints(ushort increaseAmount, GameObject pointsSource)
    {
        ushort tempPoints = CurrentPoints;
        CurrentPoints += increaseAmount;

        //call OnWinMatch action
        ushort trueWinAmount = (ushort) (CurrentPoints - tempPoints);
        if (trueWinAmount > 0)
        {
            OnWinMatch?.Invoke(trueWinAmount);
            Debug.Log("+" + trueWinAmount);
        }
    }
    [Command]
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
            OnLoseMatch?.Invoke(trueLoseAmount);
            Debug.Log("-" + trueLoseAmount);
        }

        HandleDeath();
    }
        
        
    public void Kill()
    {
        CurrentPoints = 0;
        //call OnLoseMatch action
        OnLoseMatch?.Invoke(CurrentPoints);
            
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


