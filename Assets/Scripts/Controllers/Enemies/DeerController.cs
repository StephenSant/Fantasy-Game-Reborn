using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : EnemyController
{
    public override void Update()
    {

        if (target != null)
        {
            curAction = CurAction.Flee;
        }
        else
        {
            curAction = CurAction.Idle;
        }
        base.Update();
    }
    public override void PrimaryAttack()
    {}

    public override void SecondaryAttack()
    {}
}
