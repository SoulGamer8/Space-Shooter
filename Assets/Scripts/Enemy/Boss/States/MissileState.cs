using UnityEngine;

namespace Boss{
    public class MissileState : BossState
    {
        private float _timer;
        private int _currentlyMissilesVolley;

        public MissileState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){}

        public override void OnEnter(){}

        
        public override void UpdateState(){
            _timer += Time.deltaTime;
            if(_timer >= bossController._fireRateMissile){
                    GameObject.Instantiate(bossController._missile,bossController._gunMissile[0].position,Quaternion.Euler(0,0,180));
                    GameObject.Instantiate(bossController._missile,bossController._gunMissile[1].position,Quaternion.Euler(0,0,180));
                    _timer=0;
                    _currentlyMissilesVolley++;
            }
            if(_currentlyMissilesVolley >= bossController._amountMissilesVolley)
                bossController.NextState();
        }

        public override void OnExit(){}
    }
}