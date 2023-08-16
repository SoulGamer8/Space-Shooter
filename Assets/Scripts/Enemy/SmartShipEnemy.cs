using System.Collections;
using UnityEngine;

public class SmartShipEnemy : Enemy
{
    [Header("Soot")]
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _speedBullet;
    [SerializeField] private float _timeToFire;
    [SerializeField] private float _timeToMove;


    enum MovingState{Moving,Firing,Waiting}
    MovingState _myState;

    private AudioSource _audioSource;

    private Animator _animator;
    
    private Vector2 _targetPosition;


    float duration = 5.0f;

    float startTime;

    [SerializeField] private float _timeWait;




    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
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
                DoShoot();
                break;
            case 2:
                DoWait();
                break;

        }
    }

    private void ChangeState(MovingState newState){
        startTime = 0;
        _myState = newState;
    }

    public override void Damege(int damage){
        base.Damege(damage);
    }

    protected override void DoMove(){
        startTime += Time.deltaTime;
        float percent = startTime/duration;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, percent*_speed);
        // transform.rotation =Quaternion.Lerp(transform.rotation,new Quaternion(0,0,360,0),percent);
        transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
        if(Vector3.Distance(transform.position, _targetPosition) < 0.1f){
            ChangeState(MovingState.Firing);
            SetTargetPosition();
           
        }
    }

    protected void SetTargetPosition(){
        int randomX = Random.Range(-9,9);
        int randomY = Random.Range(0,6);
        _targetPosition =  new Vector2(randomX,randomY);
    }

    protected override void DoShoot(){
        GameObject bullet;
        Vector3 target = SetTarget();
        float angelToFire = Vector3.SignedAngle(Vector3.up ,target-transform.position, Vector3.forward);
        
        bullet = Instantiate(_bullet, new Vector3(transform.position.x,transform.position.y, 0),Quaternion.Euler(0,0,angelToFire));
        bullet.GetComponent<SmartEnemyLaser>().SetDamage(_damage);
        bullet.GetComponent<SmartEnemyLaser>().SetTarget(target);
        bullet.GetComponent<SmartEnemyLaser>().SetSpeed(_speedBullet);
        ChangeState(MovingState.Waiting);
    }

    private void DoWait(){
        startTime += Time.deltaTime;
        if(startTime >= _timeWait)
            ChangeState(MovingState.Moving);
    }
    private Vector3 SetTarget(){
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        int randomPlayer = UnityEngine.Random.Range(0, player.Length);
        return player[randomPlayer].transform.position;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void Dead()
    {
        StopAllCoroutines();

        _animator.SetTrigger("IsEnemyDead");
        this.GetComponent<BoxCollider2D>().enabled = false;

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        Destroy(gameObject, 2.8f);
    }

    
}
