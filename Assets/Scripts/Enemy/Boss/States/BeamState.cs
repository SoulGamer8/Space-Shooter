using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class BeamState : BossState
{
    private float _timer;
    private float _timerFlash;
    private float _defaultRotation;
    private  Vector3 _maxChargeSixe;
    private enum BeamAttackState{Tracking,Locked,Fire}
    BeamAttackState _currentBeamAttackState;

    public BeamState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){}

    public override void OnEnter(){
        _maxChargeSixe = bossController._beamObject[0]._chargeBall.transform.localScale;
        ChangeState(BeamAttackState.Tracking);
        foreach(BeamObject beamObject in bossController._beamObject){
            beamObject._chargeBall.SetActive(true);
            CountToTenAsync(beamObject._warningLine,bossController._trackingFlashTime);
        }

        _defaultRotation =bossController._beamObject[0]._beamParent.transform.eulerAngles.z;

       
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
                Fire();
                break;
        }
    }

    #region  Traking
    private void Tracking(){
        float progress = _timer/bossController._trackingDuration;
        Vector3 chargeBallScale = Vector3.Lerp(Vector3.zero,_maxChargeSixe,progress);
        foreach(BeamObject beamObject in bossController._beamObject){
            beamObject._chargeBall.transform.localScale = chargeBallScale;  
        }

        if(bossController._playerTransform)
           RotateBeams();
        if(_timer >= bossController._trackingDuration)
            ChangeState(BeamAttackState.Locked);

    }

    void RotateBeams()
    {
        foreach (BeamObject obj in bossController._beamObject)
        {
            Vector3 directionToPlayer = bossController._playerTransform.position - obj._beamParent.transform.position;
            RotateToVector(obj._beamParent, directionToPlayer, _defaultRotation);
        }
    }

    protected static void RotateToVector(GameObject obj, Vector3 targetDirection, float defaultZ){
        float myCurrentAngle = obj.transform.rotation.eulerAngles.z - defaultZ;
        Vector3 myCurrentFacing = Quaternion.Euler(0, 0, myCurrentAngle) * Vector3.down;
        float angleToRotate = Vector3.SignedAngle(myCurrentFacing, targetDirection, Vector3.forward);
        obj.transform.rotation *= Quaternion.AngleAxis(angleToRotate, Vector3.forward);
    }
    #endregion

    void Locked(){
        if (_timer >= bossController._timetoLock)
            ChangeState(BeamAttackState.Fire);
    }

    private void Fire(){
        _timer +=Time.deltaTime;
        foreach (BeamObject obj in bossController._beamObject)
        {
            obj._chargeBall.SetActive(false);
            obj._beamAttack.SetActive(true);
        }
        if(_timer >= bossController._beamAttackTime)
            bossStateMachine.ChangeState(bossController.shootLaserState);
    }


    private async Task CountToTenAsync(GameObject warningLine,float flahTime){
        float flahTimeConvert = flahTime *1000;
        while (_currentBeamAttackState != BeamAttackState.Fire){
            await Task.Delay((int)flahTimeConvert);
            warningLine.SetActive(true);
            await Task.Delay((int)flahTimeConvert);
            warningLine.SetActive(false);
        }
    }

    private void ChangeState(BeamAttackState _newState){
        _currentBeamAttackState = _newState;
        _timer=0;
    }


    public override void OnExit(){
        foreach (BeamObject obj in bossController._beamObject){
                obj._beamAttack.SetActive(false);
            }
    }
}
