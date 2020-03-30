using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : EnemyController
{
    public override void Update()
    {
        if (health <= 10 && target == null)
        {
            curAction = CurAction.Flee;
        }
        else if (health > 10 && target != null)
        {
            curAction = CurAction.PrimaryAttack;
        }
        else
        {
            curAction = CurAction.Pander;
        }
        base.Update();
    }
    public override void PrimaryAttack()
    {
        agent.speed = runSpeed;
        if (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance + 0.5f)
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.hasPath)//if you dont have a path, make one
            {
                if (agent.CalculatePath(target.position, path))//if you cant go there, try again
                {
                    agent.SetPath(path);
                }
                else { return; }
            }
        }
        else
        {
            //Face player
            Vector3 dir = (target.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);

            RaycastHit[] hits;
            hits = Physics.SphereCastAll(transform.position, 1, transform.forward,primaryAttackRange);
            if (Timer(primaryAttackCoolDown))
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Collider hit = hits[i].collider;
                    if (hit.GetComponent<PlayerController>())
                    {
                        hit.GetComponent<PlayerController>().TakeDamage(primaryAttackDam);
                    }
                }
            }
        }
    }

    public override void SecondaryAttack()
    { }
}
