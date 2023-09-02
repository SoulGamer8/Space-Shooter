using UnityEngine;

namespace Boss{
    public class ShootingLaserState : BossState
    {
        private float _timer;

        public ShootingLaserState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){

        }

        public override void OnEnter(){
            bossController._bossMove.SetSpeed(8);
        }

        public override void UpdateState(){
            if(_timer > bossController._fireRateBullet){
                int angelBetweenLaser = 2 * bossController._volleyLaserSpread / (bossController._countLaserToFire-1);

                for(int angle = -bossController._volleyLaserSpread;angle<= bossController._volleyLaserSpread;angle += angelBetweenLaser){
                    GameObject bullet = GameObject.Instantiate(bossController._laser,bossController.transform.position,Quaternion.Euler(0,0,angle));  
                }

                _timer = 0;
            }

            _timer +=Time.deltaTime;
        }
        
        public override void OnExit(){
            bossController._bossMove.SetSpeed(0);
        }
    }
}