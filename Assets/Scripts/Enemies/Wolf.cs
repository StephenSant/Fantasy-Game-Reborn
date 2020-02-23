using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    [Header("Wolf Values")]
    public float viewDis = 8.5f;
    public int damage;
    Vector3 destination;
    Transform target;
    public bool goOutward;
    Vector3 outwardPoint;
    float distance;
    int counter;
    public bool isAttacking = false;
    float timer;

    public void Start()
    {
        enemyName = "Wolf";
    }

    public override void Update()
    {
        base.Update();

        target = player.transform;

        distance = Vector3.Distance(target.position, transform.position);

        if (Vector3.Distance(destination, transform.position) <= aI.stoppingDistance)
        {
            if (counter == 0)
            {
                Attack();
                isAttacking = true;

            }
            else
            {
                isAttacking = false;

            }
            if (!isAttacking)
            {
                timer = .5f;
                counter++;
                SetPoint();
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    counter++;
                }
            }

        }

        if (distance <= viewDis)
        {

            aI.SetDestination(destination);

        }

        if (goOutward) { destination = outwardPoint; }
        else { destination = target.position; }


    }


    public void SetPoint()
    {

        if (goOutward)
        {
            if (counter > 3)
            {
                counter = 0;
                goOutward = false;
            }
        }
        else { goOutward = true; }
        outwardPoint = FindOutwardPoint();
    }

    public Vector3 FindOutwardPoint()
    {
        try
        {
            float halfDist = (target.position - transform.position).magnitude / 2;
            Vector3 targetDir = (target.position - transform.position).normalized;
            Vector3 x = transform.position + targetDir * halfDist;

            Vector3 outwardDir = new Vector3(-targetDir.z, 0, targetDir.x);
            Vector3 y = transform.position + outwardDir * halfDist;
            return transform.position + (x - y).normalized * 5f;
        }
        catch { return Vector3.zero; }
    }

    public void Attack()
    {
        //bool hasShield = false;
        bool hasPlayer = false;

        PlayerController player = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward, 1);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].GetComponent<PlayerController>())
            {
                //hasPlayer = true;
                player = hitColliders[i].GetComponent<PlayerController>();
            }
            //if (hitColliders[i].CompareTag("Shield"))
            //{
            //    hasShield = true;
            //}
            i++;
        }

        Debug.Log(player);

        if (hasPlayer)
        {
            player.TakeDamage(damage);
            counter++;
        }


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDis);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(FindOutwardPoint(), .5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1);
    }

}
