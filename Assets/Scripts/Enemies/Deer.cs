using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Enemy
{
   /* [Header("Deer Values")]
    public float viewDis = 8.5f;
    public float minTurnFrequency = .5f;
    public float maxTurnFrequency = 1;
    public int turnPos;
    public float runningTime = 10;
    public float curRunTime;
    float turnTime;
    Vector3 destination;

    private void Start()
    {
        enemyName = "Deer";
    }

    public override void Update()
    {
        destination = transform.position + (transform.position - player.transform.position).normalized;
        base.Update();

        if (turnTime <= 0)
        {
            Turn();
            turnTime = Random.Range(minTurnFrequency, maxTurnFrequency);
        }
        turnTime -= Time.deltaTime;

        if ((transform.position - player.transform.position).magnitude < viewDis)
        {
            curRunTime = runningTime;
        }
        else
        {
            curRunTime -= Time.deltaTime;
        }

        if (curRunTime < 0)
        {
            curRunTime = 0;
        }

        if (curRunTime > 0)
        {
            RunAway();
        }
    }
    public void Turn()
    {
        turnPos = Random.Range(-1, 2);
    }

    public void RunAway()
    {
        aI.SetDestination(destination + transform.right * turnPos + transform.forward);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewDis);
    }*/
}
