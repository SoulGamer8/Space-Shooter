using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamState : BossState
{
    private float _timer;
    private float _defaultRotation;
    private  Vector3 _maxChargeSixe;
    private enum BeamAttackState{Tracking,Locked,Fire}
    BeamAttackState _currentBeamAttackState;

    public BeamState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine)
    {
    }

    public override void OnEnter()
    {


        _maxChargeSixe = bossController._beamObject[0]._chargeBall.transform.localScale;
        ChangeState(BeamAttackState.Tracking);
        foreach(BeamObject beamObject in bossController._beamObject){
            beamObject._chargeBall.SetActive(true);
            WarningFlashRoutine(beamObject._warningLine);
        }

        _defaultRotation =bossController._beamObject[0]._beamParent.transform.eulerAngles.z;

       
    }

    public override void UpdateState()
    {
        _timer += Time.deltaTime;
        switch((int)_currentBeamAttackState){
            case 0:
                Debug.Log("Tracking");
                Tracking();
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
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

        // void DoLocked()
        // {
        //     float timetoLock = _myBehavior.onPhaseTwo ? _lockedOnDurationPhaseTwo : _lockedOnDuration;
        //     if (_elapsedTime >= timetoLock)
        //         ChangeState(BeamAttackState.Fire);
        // }

        // void DoFire()
        // {
        //     if (_elapsedTime < _firingDuration) return;
        //     if (_myBehavior.onPhaseTwo && _beamVolleysFired < _beamVolleysPhaseTwo)
        //     {
        //         ChangeState(BeamAttackState.Tracking);
        //         TurnOffBeams();
        //     }
        //     else
        //         _myBehavior.SwitchState(_myBehavior.laserTrackingState);
        // }

        // void FireBeams()
        // {
        //     _beamVolleysFired++;
        //     foreach (BeamObject obj in _beamObjects)
        //     {
        //         obj._chargeBall.SetActive(false);
        //         obj._beamAttack.SetActive(true);
        //     }
        //     _myBehavior.PlaySound(_myBehavior._projectileAudio, 0.4f);
        // }

        // void TurnOffBeams()
        // {
        //     foreach (BeamObject obj in _beamObjects)
        //     {
        //         obj._beamAttack.SetActive(false);
        //     }
        // }

    private void Locked(){

    }

    private void Fire(){

    }


    private void ChangeState(BeamAttackState _newState){
        _currentBeamAttackState = _newState;
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator WarningFlashRoutine(GameObject warningLine){
        WaitForSeconds slowFlash = new WaitForSeconds(bossController._trackingFlashTime);
        WaitForSeconds fastFlash = new WaitForSeconds(bossController._lockedFlashTime);
        Debug.Log(_currentBeamAttackState != BeamAttackState.Fire);
        while(_currentBeamAttackState != BeamAttackState.Fire){
           
            yield return _currentBeamAttackState == BeamAttackState.Locked ? fastFlash : slowFlash;
            warningLine.SetActive(true);
            yield return _currentBeamAttackState == BeamAttackState.Locked ? fastFlash : slowFlash;
            warningLine.SetActive(false);
        }
    }

}
