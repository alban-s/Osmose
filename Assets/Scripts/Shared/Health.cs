using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using Osmose.Game;

public class Health : NetworkBehaviour
{
    [Tooltip("Starting amount of points")] public ushort StartPoints;
    [Tooltip("Current amount of points")]public ushort CurrentPoints;

    public bool isTest;
    public UnityAction<ushort> OnLoseMatch;
    public UnityAction<ushort> OnWinMatch;
    public UnityAction OnDie;
    private GameObject GameManager;

    private Animator animator;
    [SerializeField]
    private GameObject laser;
    private GameObject instance;

    public bool Invincible { get; set; }
    public bool IsActive { get; set; }
    public bool IsInBase { get; set; }
    public ushort GetPoints() => CurrentPoints;

    bool m_IsDead;

    void Start()
    {
        StartPoints = 0;
        animator = GetComponentInChildren<Animator>();
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
        /*m_IsDead = true;
        if(m_IsDead){
            dead();
            StartCoroutine(wait());
        }*/
    }

    [Command]
    void update_score_clients(){
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
        foreach (GameObject player in players)
        {
            player.GetComponent<Health>().rpc_update_score_client(CurrentPoints);
        }
    }

    [ClientRpc]
    void rpc_update_score_client(ushort score){
        Debug.Log("cp : " + score);
        CurrentPoints = score;
        Debug.Log("cp : " + GetComponent<Health>().CurrentPoints);
    }

    [Command]
    public void SetStartPoints(ushort startPoints)
    {
        StartPoints = startPoints;
        CurrentPoints = StartPoints;
        update_score_clients();
    }

    [Command]
    public void IncreasePoints(ushort increaseAmount, GameObject pointsSource)
    {
        ushort tempPoints = CurrentPoints;
        CurrentPoints += increaseAmount;
        update_score_clients();

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
        update_score_clients();

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
        update_score_clients();
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


