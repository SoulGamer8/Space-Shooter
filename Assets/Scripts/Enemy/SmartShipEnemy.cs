using UnityEngine;

public class SmartShipEnemy : Enemy
{
    [Header("Soot")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _speedBullet;


    enum MovingState{Moving,Firing,Waiting}
    MovingState _myState;


    private Vector2 _targetPosition;


    private float duration = 5.0f;

    private float _timer;

    [SerializeField] private float _timeWait;


    private void Awake(){
        SetTargetPosition();
        ChangeState(MovingState.Moving);
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
                Shoot();
                break;
            case 2:
                DoWait();
                break;

        }
    }

    private void ChangeState(MovingState newState){
        _timer = 0;
        _myState = newState;
    }

    public void DoMove(){
        float percent = _timer/duration;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, percent*_speed);

        transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
        if(Vector3.Distance(transform.position, _targetPosition) < 0.1f){
            ChangeState(MovingState.Firing);
            SetTargetPosition();
           
        }
    }

    private void SetTargetPosition(){
        int randomX = Random.Range(-9,9);
        int randomY = Random.Range(0,6);
        _targetPosition =  new Vector2(randomX,randomY);
    }

    public void Shoot(){
        GameObject bullet;
        Vector3 target = SetTarget();
        float angelToFire = Vector3.SignedAngle(Vector3.up ,target-transform.position, Vector3.forward);
        
        bullet = Instantiate(_bullet, new Vector3(transform.position.x,transform.position.y, 0),Quaternion.Euler(0,0,angelToFire));
        bullet.GetComponent<SmartEnemyLaser>().SetDamage(_damage);
        bullet.GetComponent<SmartEnemyLaser>().SetSpeed(_speedBullet);
        ChangeState(MovingState.Waiting);
    }

    private void DoWait(){
        _timer += Time.deltaTime;
        if(_timer >= _timeWait)
            ChangeState(MovingState.Moving);
    }
    private Vector3 SetTarget(){
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        int randomPlayer = UnityEngine.Random.Range(0, player.Length);
        return player[randomPlayer].transform.position;
    }

    public override int GetSpawnChanceWeight()
    {
       return _spawnChanceWeight;
    }
}
