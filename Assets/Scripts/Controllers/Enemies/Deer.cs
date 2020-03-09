using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : EnemyController
{
    Transform tempTarget;
    public override void Update()
    {

        if (tempTarget != target)
        {
            curAction = CurAction.Flee;
        }

        tempTarget = target;
        base.Update();
    }
}
