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
                Wander();
                break;
            case CurAction.Approach:
                Approach();
                break;
            default:
                Idle();
                break;
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
    }

    void Wander()
    {

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

    public void Die()
    {
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