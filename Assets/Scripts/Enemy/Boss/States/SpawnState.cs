using UnityEngine;

namespace Boss{
    public class SpawnState : BossState
    {
        private Transform _transform;
        private float _speed;

        public SpawnState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine)
        {
        }

        public override void OnEnter(){
            _transform = bossController.gameObject.transform;
            _speed = bossController._bossSpeed;

        }

        public override void OnExit(){}

        public override void UpdateState(){
            _transform.position = Vector3.MoveTowards(_transform.position,bossController._target,0.2f* _speed);
            if(Vector3.Distance(_transform.position,bossController._target)<0.1f){
                bossStateMachine.ChangeState(bossController.shootLaserState);
                bossController.NextState();
            }
        }
    }
}