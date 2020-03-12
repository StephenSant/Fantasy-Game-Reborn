using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public CurAction curAction = CurAction.Idle;
    [Header("References")]
    public SphereCollider viewCollider;
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public NavMeshAgent agent;
    public Transform target; //usally the player 
    public Vector3 destination; //the point to move to
    [Header("Fleeing")]
    public Vector2 fleeTurnFreq = new Vector2(0.5f, 1); //how often does it turn
    public float fleeTime; //how long do they run for
    [Header("Wandering")]
    [Tooltip("Also used in Ponder")]
    public float wanderWaitTime;
    [Header("Pondering")] [Tooltip("Patrols and wanders ")]
    public Vector3 ponderPoint;
    public float ponderRadius;
    Vector3 ponderTarget;
    [Header("Primary Attack")]
    [Header("Secondary Attack")]
    [Header("Health")]
    public int maxHealth;
    public int health;
    [Header("Combat")]
    public float viewDist;
    public string[] targetNames;
    public int primaryAttackDam;
    public int secondaryAttackDam;

    private void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        health = maxHealth;

        ponderPoint = transform.position;

        //Makes view checker
        viewCollider = gameObject.AddComponent<SphereCollider>();
        viewCollider.isTrigger = true;
    }


    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Update()
    {
        //#if UNITY_EDITOR  
        #region For Testing
        if (viewCollider.radius != viewDist)
        {
            viewCollider.radius = viewDist;
        }
        #endregion
        //#endif



        switch (curAction)
        {
            case CurAction.Flee:
                Flee();
                break;
            case CurAction.Wander:
                Wander();
                break;
            case CurAction.Ponder:
                Ponder();
                break;
            case CurAction.PrimaryAttack:
                PrimaryAttack();
                break;
            case CurAction.SecondaryAttack:
                SecondaryAttack();
                break;
            default:
                Idle();
                break;
        }
    }

    public void Idle()
    {
        //Just run the idle animation
    }

    public void Flee()
    {
        agent.speed = runSpeed;
        agent.SetDestination(transform.position + (transform.position - target.position).normalized * 5 + (transform.right * Random.Range(-6f, 6f)));
    }

    public void Wander()
    {
        agent.speed = walkSpeed;
        agent.SetDestination(transform.position + (transform.forward * Random.Range(-1f, 3f)) + (transform.right * Random.Range(-5f, 5f)));
    }

    float ponderT;
    public void Ponder()
    {
        agent.speed = walkSpeed;
        if (ponderT <= 0)
        {
            ponderT = wanderWaitTime;
            ponderTarget = transform.position + new Vector3(Random.Range(-ponderRadius, ponderRadius), 0, Random.Range(-ponderRadius, ponderRadius));
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            ponderT -= Time.deltaTime;
        }
        agent.SetDestination(ponderTarget);
    }

    public virtual void PrimaryAttack()
    {
        agent.speed = runSpeed;
    }

    public virtual void SecondaryAttack()
    {
        agent.speed = runSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        InSight(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OutSight(other);
    }

    void InSight(Collider other)
    {
        for (int i = 0; i < targetNames.Length; i++)
        {
            if (other.name == targetNames[i] && target == null)
            {
                target = other.transform;
            }
        }
    }

    void OutSight(Collider other)
    {
        for (int i = 0; i < targetNames.Length; i++)
        {
            if (other.name == target.name)
            {
                target = null;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Delt " + damage + " damage to " + gameObject.name + ".");
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " was slain.");
        Destroy(gameObject);
    }

}
public enum CurAction
{
    Flee,
    Wander,
    Ponder,
    PrimaryAttack,
    SecondaryAttack,
    Idle
}