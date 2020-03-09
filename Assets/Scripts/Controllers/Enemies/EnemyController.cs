using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public CurAction curAction;
    [Header("References")]
    public SphereCollider viewCollider;
    [Header("Movement")]
    public float moveSpeed;
    public NavMeshAgent agent;
    public Transform target; //usally the player 
    public Vector3 destination; //the point to move to
    [Header("Fleeing")]
    public Vector2 fleeTurnFreq = new Vector2(0.5f, 1); //how often does it turn
    public float fleeTime; //how long do they run for
    [HideInInspector]
    public float fleeTempTime; //how long have they been running for 
    float fleeTurnPos; //where it's turns towards
    float fleeTurnTime; //how long until it turns again
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

        //Makes view checker
        viewCollider = gameObject.AddComponent<SphereCollider>();
        viewCollider.isTrigger = true;
    }

    public virtual void Update()
    {
        //#if UNITY_EDITOR  
        #region For Testing
        if (agent.speed != moveSpeed)
        {
            agent.speed = moveSpeed;
        }
        if (viewCollider.radius != viewDist)
        {
            viewCollider.radius = viewDist;
        }
        #endregion
        //#endif

        switch (curAction)
        {
            case CurAction.Flee:
               agent.speed = moveSpeed;
                if (target != null)
                {
                    Flee();
                }
                else
                {
                    curAction = CurAction.Idle;
                }
                break;
            case CurAction.Wander:
                agent.speed = moveSpeed / 2;

                StartCoroutine(Wander());
                break;
            case CurAction.Approach:
                agent.speed = moveSpeed;
                Approach();
                break;
            default:
                Idle();
                break;
        }

        if (health <= 0)
        {
            Die();
        }

    }

    void Flee()
    {
        fleeTempTime += Time.deltaTime;
        destination = transform.position + (transform.position - target.position).normalized;

        if (fleeTurnTime <= 0)
        {
            fleeTurnPos = Random.Range(-1, 2);
            fleeTurnTime = Random.Range(fleeTurnFreq.x, fleeTurnFreq.y);
        }
        fleeTurnTime -= Time.deltaTime;

        agent.SetDestination(destination + transform.right * fleeTurnPos + transform.forward);

        if (fleeTempTime >= fleeTime)
        {
            fleeTempTime = 0;
            target = null;
            agent.SetDestination(transform.position);
        }

    }

    IEnumerator Wander()
    {
        Debug.Log("Yeet");
        float waitTime;
        waitTime = Random.Range(0f,5f);
        new WaitForSeconds(waitTime);
        destination = transform.position + new Vector3(Random.Range(-5f,5f),0, Random.Range(-5f,5f));
        agent.SetDestination(destination);
        return null;
    }

    void Approach()
    {
        
    }

    void Idle()
    {
        //Run idle animation
    }

    private void OnTriggerStay(Collider other)
    {
        CheckSight(other);
    }

    void CheckSight(Collider other)
    {
        for (int i = 0; i < targetNames.Length; i++)
        {
            if (other.name == targetNames[i] && target == null)
            {
                target = other.transform;
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
    Approach,
    Idle
}