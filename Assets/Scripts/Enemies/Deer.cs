using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : EnemyController
{
    public override void Update()
    {

        if (fleeTempTime >= fleeTime)
        {
            fleeTempTime = 0;
            target = null;
            curAction = CurAction.Idle;
        }

        else
        {
            curAction = CurAction.Flee;
        }

        base.Update();
    }
}
