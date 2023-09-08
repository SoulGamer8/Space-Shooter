using UnityEngine;

public class BeamShipEnemy : Enemy, IMoveable, ICanShoot
{
    [SerializeField] private BeamObject _beamObject;
    [SerializeField] private float _rotateTime;
    [SerializeField] private float _timeFiring;

    enum MovingState{Moving,Rotating,Firing};
    MovingState _myState;

    private float _timer;
    private Vector3 _targetPosition;
    private float _targetRotation;
    private Transform _playerTransform;
    
    private Vector3 _maxChargeBall;
    
    private void ChangeState(MovingState newState){
        _timer = 0;
        _myState = newState;
    }

    public override  void Start() {
        base.Start();
        
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _maxChargeBall = _beamObject._chargeBall.transform.localScale;

        SetTargetPosition();
    }

    private void Update() {
        Act();
    }

    private void Act(){
        switch((int)_myState){
            case 0:
                DoMove();
                break;
            case 1:
                RotateShip();
                break;
            case 2:
                Shoot();
                break;
        }
    }

    public void DoMove(){
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.1f*_speed);

        if(Vector3.Distance(transform.position, _targetPosition) < 0.1f){
            _beamObject._warningLine.SetActive(true);
            _beamObject._chargeBall.SetActive(true);

            ChangeState(MovingState.Rotating);
            SetTargetPosition();
        }
            
    }

    private void SetTargetPosition(){
        int randomX = Random.Range(-9,9);
        int randomY = Random.Range(0,6);
        _targetPosition =  new Vector2(randomX,randomY);
    }

    private void RotateShip(){
        _timer +=Time.deltaTime;
        _targetRotation = Vector3.SignedAngle(Vector3.up ,_playerTransform.transform.position-transform.position, Vector3.forward);
        transform.rotation =  Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,_targetRotation),0.1f);
        _beamObject._chargeBall.transform.localScale = Vector3.Lerp(Vector3.zero,_maxChargeBall,0.1f);

        if(_timer >_rotateTime){
            _beamObject._warningLine.SetActive(false);
            _beamObject._beamAttack.SetActive(true);
            _beamObject._chargeBall.SetActive(false);

            ChangeState(MovingState.Firing);
        }
    }

    public void Shoot(){
        _timer += Time.deltaTime;
        if(_timer > _timeFiring){
            _beamObject._beamAttack.SetActive(false);
            _beamObject._warningLine.SetActive(true);

            ChangeState(MovingState.Moving);
        }
    }
}
