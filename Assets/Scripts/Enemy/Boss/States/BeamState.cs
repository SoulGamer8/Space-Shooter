using System.Threading.Tasks;
using UnityEngine;

namespace Boss{
    public class BeamState : BossState
    {
        private float _timer;
        private float _timerFlash;
        private float _defaultRotation;
        private  Vector3 _maxChargeSize;
        private int _currentlyAmountVolleyBeam;

        private enum BeamAttackState{Tracking,Locked,Fire}
        BeamAttackState _currentBeamAttackState;

        public BeamState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){}

        public override void OnEnter(){
            _maxChargeSize = bossController._beamObject[0]._chargeBall.transform.localScale;
            _defaultRotation =bossController._beamObject[0]._beamParent.transform.eulerAngles.z;  

            StartTracking();
        }

        public override void UpdateState(){
            _timer += Time.deltaTime;
            switch((int)_currentBeamAttackState){
                case 0:
                    Tracking();
                    break;
                case 1:
                    Locked();
                    break;
                case 2:
                    bossController._bossAudioManager.PlaySound(bossController._soundBeamAttack);
                    Fire();
                    break;
            }
        }

        #region Tracking

        private void StartTracking(){
            if( _currentlyAmountVolleyBeam >= bossController._amountBeamVolley)
                bossStateMachine.ChangeState(bossController.missileState);
            foreach(BeamObject beamObject in bossController._beamObject){
                beamObject._chargeBall.SetActive(true);
                beamObject._warningLine.SetActive(true);
                TrackFlashingAsync(beamObject._warningLine,bossController._trackingFlashTime);
            }
            ChangeState(BeamAttackState.Tracking);
        }

        private void Tracking(){
            float progress = _timer/bossController._trackingDuration;
            Vector3 chargeBallScale = Vector3.Lerp(Vector3.zero,_maxChargeSize,progress);
            foreach(BeamObject beamObject in bossController._beamObject){
                beamObject._chargeBall.transform.localScale = chargeBallScale;  
            }

            if(bossController._playerTransform)
            RotateBeams();
            if(_timer >= bossController._trackingDuration)
                ChangeState(BeamAttackState.Locked);

        }

        private void RotateBeams()
        {
            foreach (BeamObject obj in bossController._beamObject)
            {
                Vector3 directionToPlayer = bossController._playerTransform.position - obj._beamParent.transform.position;
                RotateToVector(obj._beamParent, directionToPlayer, _defaultRotation);
            }
        }

        private void RotateToVector(GameObject obj, Vector3 targetDirection, float defaultZ){
            float myCurrentAngle = obj.transform.rotation.eulerAngles.z - defaultZ;
            Vector3 myCurrentFacing = Quaternion.Euler(0, 0, myCurrentAngle) * Vector3.down;
            float angleToRotate = Vector3.SignedAngle(myCurrentFacing, targetDirection, Vector3.forward);
            obj.transform.rotation *= Quaternion.AngleAxis(angleToRotate, Vector3.forward);
        }
        #endregion

        private void Locked(){
            if (_timer >= bossController._timeToLock)
                ChangeState(BeamAttackState.Fire);
        }

        private void Fire(){
            _timer +=Time.deltaTime;
            foreach (BeamObject beamObject in bossController._beamObject){
                beamObject._chargeBall.SetActive(false);
                beamObject._beamAttack.SetActive(true);
            }
            if(_timer >= bossController._beamAttackTime){
                _currentlyAmountVolleyBeam++;
                TurnOffBeams();    
                StartTracking();
            }
        
        }

        private void TurnOffBeams(){
            foreach(BeamObject beamObject in bossController._beamObject){
                beamObject._beamAttack.SetActive(false);
            }
        }

        private async void TrackFlashingAsync(GameObject warningLine,float flashTime){
            float flashTimeConvert = flashTime *1000;
            while (_currentBeamAttackState != BeamAttackState.Fire){
                await Task.Delay((int)flashTimeConvert);
                warningLine.SetActive(true);
                await Task.Delay((int)flashTimeConvert);
                warningLine.SetActive(false);
            }
        }

        private void ChangeState(BeamAttackState _newState){
            _currentBeamAttackState = _newState;
            _timer=0;
        }

        public override void OnExit(){
            foreach (BeamObject beamObject in bossController._beamObject){
                    beamObject._beamParent.SetActive(false);
                }
        }
    }
}