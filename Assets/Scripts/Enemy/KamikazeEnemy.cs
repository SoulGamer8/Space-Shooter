using UnityEngine;

public class KamikazeEnemy : Enemy
{

    [Header("Enemy Settings")]
    [SerializeField] private float _timeWait =1f;

    enum MovingState{Moving,Rotating,Waitint}
    MovingState _myState;

    private GameObject _player;
    private Vector3 _targetPosition;
    private float _targetRotation;
    float duration = 10.0f;
    float startTime;

    private void Start(){
        _player =  GameObject.FindGameObjectWithTag("Player");
    }

    public override void Damege(int damage)
    {
       base.Damege(damage);
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
                DoRotate();
                break;
            case 2:
                DoWait();
                break;
        }
    }
    
    private void ChangeState(MovingState state){
        startTime = 0;

        if(state == MovingState.Moving)
            SetTarget();
        if(state == MovingState.Rotating)
            SetRotationAngel();

        _myState = state;
    }

    public override void DoMove()
    {
        startTime += Time.deltaTime;
        float percent = startTime/duration;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, percent*_speed);

        if(Vector3.Distance(transform.position, _targetPosition)<0.01f){
            ChangeState(MovingState.Waitint);
        }
    }

    
    private void DoRotate(){
        startTime += Time.deltaTime;
        float percent = startTime/duration;

        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,_targetRotation),percent);

        float diff = transform.rotation.eulerAngles.z - Quaternion.Euler(0,0,_targetRotation).eulerAngles.z;

        if (Mathf.Abs (diff) <= 1)
            ChangeState(MovingState.Moving);
    }

    private void DoWait(){
        startTime += Time.deltaTime;
        if(startTime >= _timeWait)
            ChangeState(MovingState.Rotating);
    }

    private void SetTarget(){
        _targetPosition =_player.transform.position;
    }

    private void SetRotationAngel(){
        _targetRotation = Vector3.SignedAngle(Vector3.up ,_player.transform.position-transform.position, Vector3.forward);
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(collider.tag== "Player"){
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void Dead()
    {
        Destroy(gameObject);
    }

    public override int GetSpawnChanceWeight()
    {
       return _spawnChanceWeight;
    }
}
