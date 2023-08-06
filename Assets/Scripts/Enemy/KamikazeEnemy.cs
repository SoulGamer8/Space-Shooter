using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy
{

    [Header("Enemy Settings")]
    [SerializeField] private int _health=3;

    [SerializeField] private float _speed=4;

    [SerializeField] private int _damage=1;

    [SerializeField] private int _score = 1;

    [SerializeField] private float _timeWait =1f;

    [Header("PowerUp")]
    [RangeAttribute(0,1f)]
    [SerializeField] private float _chanceSpawnTripleShotPowerUp;
    [SerializeField] private GameObject[] _powerUps;


    [Header("Dead")]
    [SerializeField] private AudioClip _explosionSound;


    enum MovingState{Moving,Rotating,Waitint}
    MovingState _myState;

    private GameObject _player;
    private Vector3 _targetPosition;
    private float _targetRotation;
    float duration = 5.0f;
    float startTime;

    private void Start(){
        _player =  GameObject.FindGameObjectWithTag("Player");
    }

    public override void Damege(int damage)
    {
        _health-=damage;
        if(_health <0)
            Dead();
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
        if(state == MovingState.Moving){
            SetTarget();
        }

        _myState = state;
    }

    protected override void DoMove()
    {
        startTime += Time.deltaTime;
        float percent = startTime/duration;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, percent*_speed);

        if(Vector3.Distance(transform.position, _targetPosition)<0.01f){
            ChangeState(MovingState.Rotating);
        }
    }



    private void DoRotate(){
        startTime += Time.deltaTime;
        float percent = startTime/duration;

        transform.rotation = Quaternion.Slerp(transform.rotation,new Quaternion(0,0,_targetRotation,0),startTime);
        SetRotationAngel();
    }

    private void DoWait(){

    }

    private void SetTarget(){
        _targetPosition =_player.transform.position;
    }

    private void SetRotationAngel(){
        _targetRotation = Vector3.SignedAngle(Vector3.up ,_player.transform.position-transform.position, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(other.tag== "Player"){
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void Dead()
    {
        Destroy(gameObject);
    }



    protected override void DoShoot()
    {
        throw new System.NotImplementedException();
    }

}
