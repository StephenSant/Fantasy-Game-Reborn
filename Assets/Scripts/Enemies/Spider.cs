using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    Transform target;
    public float viewDis = 8.5f;
    public int damage;
    float distance;
    public bool isAttacking = false;
    float timer;

    public override void Update()
    {
        base.Update();
        if (GameManager.instance.player != null)
        {
            try
            {
                target = GameManager.instance.player.transform;
            }
            catch
            { }

        }
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= viewDis)
        {

            aI.SetDestination(target.position);

        }
        timer -= Time.deltaTime;
        if (Vector3.Distance(target.position, transform.position) <= aI.stoppingDistance)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            if (!isAttacking)
            {
                Attack();
                isAttacking = true;
                timer = .5f;
            }
            else
            {

                if (timer <= 0)
                {
                    isAttacking = false;
                }
            }
        }
    }
    public void Attack()
    {
        bool hasShield = false;
        bool hasPlayer = false;

        PlayerController player = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward*0.5f, 0.5f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].GetComponent<PlayerController>())
            {
                hasPlayer = true;
                player = hitColliders[i].GetComponent<PlayerController>();
            }
            if (hitColliders[i].CompareTag("Shield"))
            {
                hasShield = true;
            }
            i++;
        }

        if (!hasShield)
        {
            if (hasPlayer)
            {
                //player.TakeDamage(damage);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewDis);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 0.5f, .5f);
    }
}
