using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : EnemyController
{
    public override void Update()
    {
        
        if (health <= 10)
        {
            curAction = CurAction.Flee;
        }
        else
        {
            curAction = CurAction.Ponder;
        }

        base.Update();
    }

   
}
