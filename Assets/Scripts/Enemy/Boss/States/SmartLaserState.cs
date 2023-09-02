
using UnityEngine;

namespace Boss{
    public class SmartLaserState : BossState
    {
        private float _timer;


        public SmartLaserState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine)
        {
        }

        public override void OnEnter()
        {
        }

        public override void UpdateState(){
            _timer +=Time.deltaTime;
            if(_timer >= bossController._fireRateSmartLaser){
                for(int i = 0;i< bossController._gunLaser.Length;i++){
                    GameObject laser = GameObject.Instantiate(bossController._smartLaser,bossController._gunLaser[i].transform.position,Quaternion.identity);
                }
                _timer = 0;
            }

        }
        
        public override void OnExit()
        {
        }

    }
}