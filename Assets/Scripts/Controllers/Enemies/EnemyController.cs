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
    [Tooltip("Also used in pander")]
    public float wanderWaitTime;
    [Header("Pandering")]
    public Vector3 panderPoint;
    public float panderRadius;
    Vector3 panderTarget;
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

        panderPoint = transform.position;

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
            case CurAction.Pander:
                Pander();
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
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(transform.position + (transform.position - target.position).normalized * 5 + (transform.right * Random.Range(-6f, 6f)), path))
        {
            agent.SetPath(path);
        }
        else { return; }
    }

    public void Wander()
    {
        agent.speed = walkSpeed;
        NavMeshPath path = new NavMeshPath();
        if (agent.hasPath)//if you dont have a path, make one
        {
            if (agent.remainingDistance <= agent.stoppingDistance)// if your at the position, get a new position
            {
                if (agent.CalculatePath(transform.position + (transform.forward * Random.Range(-1f, 3f)) + (transform.right * Random.Range(-5f, 5f)), path))//if you cant go there, try again
                {
                    agent.SetPath(path);
                }
                else { return; }
            }
        }
        else
        {
            agent.CalculatePath(transform.position + (transform.forward * Random.Range(-1f, 3f)) + (transform.right * Random.Range(-5f, 5f)), path);
            agent.SetPath(path);
        }
    }

    public void Pander()
    {
        agent.speed = walkSpeed;
        NavMeshPath path = new NavMeshPath();
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (Timer(wanderWaitTime))
            {
                panderTarget = transform.position + new Vector3(Random.Range(-panderRadius, panderRadius), 0, Random.Range(-panderRadius, panderRadius));
                if (agent.CalculatePath(panderTarget, path))
                {
                    agent.SetPath(path);
                }
                else { return; }
            }
        }

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
                try
                {
                    target = null;
                }
                catch { return; }
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

    float t;
    bool setUp= false;
    public bool Timer(float waitTime)
    {
        if (setUp)
        {
            if (t <= 0)
            {
                setUp = false;
                return true;
            }
            else
            {
                t -= Time.deltaTime;
                return false;
            }
        }
        else
        {
            t = waitTime;
            setUp = true;
            return false;
        }
    }

}
public enum CurAction
{
    Flee,
    Wander,
    Pander,
    PrimaryAttack,
    SecondaryAttack,
    Idle
}