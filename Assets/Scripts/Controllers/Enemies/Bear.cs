using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : EnemyController
{
    public override void Update()
    {
        
        if (health <= 10)
        {
            curAction = CurAction.Flee;
        }
        else if (target != null)
        {
            curAction = CurAction.Approach;
        }
        else
        {
            curAction = CurAction.Wander;
        }

        base.Update();
    }

   
}
