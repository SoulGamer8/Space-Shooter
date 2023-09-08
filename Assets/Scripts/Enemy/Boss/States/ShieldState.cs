using UnityEngine;

namespace Boss{
    public class ShieldState : BossState
    {
        private float _timer;
        private int _currentlyShieldGenerator = 2;
        private bool _isShieldSpawn= false;

        public ShieldState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){}

        public override void OnEnter(){
            bossController._shield.SetActive(true);

        }


        private void SpawnShieldGenerator(){
            GameObject.Instantiate(bossController._shieldGenerator,new Vector3(8,10,0),Quaternion.Euler(0,0,0),bossController.transform);
            GameObject.Instantiate(bossController._shieldGenerator,new Vector3(-8,10,0),Quaternion.Euler(0,0,0),bossController.transform);
            _isShieldSpawn = true;
        }

        public void ShieldGeneratorDestroy(){
            _currentlyShieldGenerator--;
            WeekShield();
            
            if(_currentlyShieldGenerator<=0)
            bossStateMachine.ChangeState(bossController.beamState);
        }

        private void WeekShield(){
            Color tmp = bossController._shield.GetComponent<SpriteRenderer>().color;
            tmp.a = bossController._weekShieldOpacity;
            bossController._shield.GetComponent<SpriteRenderer>().color = tmp;
        }

        public override void UpdateState(){
            bossController.transform.position = Vector3.MoveTowards(bossController.transform.position,bossController._target,0.1f);
            if(Vector3.Distance(bossController.transform.position,bossController._target)<0.1f && !_isShieldSpawn)
                SpawnShieldGenerator();
            if(_isShieldSpawn)
                Repair();
        }

        private void Repair(){
            _timer += Time.deltaTime;
            if(_timer >= 1){
                bossController._bossHealth._currentlyHealth +=1;
                bossController._bossHealth.UpdateHealthBar();
                _timer =0;    
            }
        }

        public override void OnExit(){
            bossController._shield.SetActive(false);
            bossController._isShieldActive = false;
        }
    }
}