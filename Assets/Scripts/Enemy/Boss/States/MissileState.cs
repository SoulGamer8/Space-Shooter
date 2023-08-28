using UnityEngine;

public class MissileState : BossState
{
    private float _timer;
    private int _curentlyMissilesVolley;

    public MissileState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){}

    public override void OnEnter(){
    }

    
    public override void UpdateState(){
        _timer += Time.deltaTime;
        if(_timer >= bossController._fireRateMissile){
                GameObject.Instantiate(bossController._missile,bossController._spawnMissile[0].position,Quaternion.Euler(0,0,180));
                GameObject.Instantiate(bossController._missile,bossController._spawnMissile[1].position,Quaternion.Euler(0,0,180));
                _timer=0;
                _curentlyMissilesVolley++;
        }
        if(_curentlyMissilesVolley >= bossController._amountMissilesVolley)
            bossStateMachine.ChangeState(bossController.shootLaserState);
    }

    public override void OnExit(){
    }

}
