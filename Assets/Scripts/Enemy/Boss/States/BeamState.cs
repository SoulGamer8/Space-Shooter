using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamState : BossState
{

    private enum BeamAttackState{Tracking,Locked,Fire}
    BeamAttackState _currentBeamAttackState;
    public BeamState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine)
    {
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
