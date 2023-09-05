using System.Collections;
using UnityEngine;

public class TripleShootEnemy : Enemy
{

    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private int _bulletSpeed;
    [SerializeField] private float _fireRate;

    private Vector3 _targetPosition;
    private Vector3 _patruling;

    enum MovingState{Starting,Moving}
    private MovingState _myState;
    private int _volleyLaserSpread = 40;
    private int _countLaserToFire = 3;

    private void Start() {
        _targetPosition = new Vector3(transform.position.x,4);
    }

    private void Update() {
        Act();
    }

    private void Act(){
        switch((int)_myState){
            case 0:
                StartMoving();
                break;
            case 1:
                DoMove();
                break;
        }
    }

    private void ChangeState(MovingState myState){
        _myState = myState;
        _patruling = new Vector3(6,transform.position.y);
    }

    private void StartMoving(){
        
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.01f);

        if(Vector3.Distance(transform.position, _targetPosition) < 0.1f){
            ChangeState(MovingState.Moving);
            Shoot();
        }
    }

    public void DoMove(){

        transform.position = Vector3.Lerp(transform.position, _patruling, 0.3f*_speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, _patruling) < 0.1f)
            _patruling.x *=-1;
    }

    public void Shoot(){
        StartCoroutine(ShootinCorutine());
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        throw new System.NotImplementedException();
    }

    protected override void Dead(){
        throw new System.NotImplementedException();
    }

    public override int GetSpawnChanceWeight(){
       return _spawnChanceWeight;
    }

    IEnumerator ShootinCorutine(){
        while(true){
            int angelBetweenLaser = 2 * _volleyLaserSpread / (_countLaserToFire-1);

            for(int angle = -_volleyLaserSpread;angle<= _volleyLaserSpread;angle += angelBetweenLaser){
                Instantiate(_bullet,transform.position,Quaternion.Euler(0,0,angle));  
        }

            yield return new WaitForSeconds(_fireRate);
        }
    }
}
