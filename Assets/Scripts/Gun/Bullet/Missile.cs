using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : Ammo ,IDamageable
{
    [SerializeField] private bool _isEnemyMissile;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 200f;
    [SerializeField] private int _damage;
    [SerializeField] private int _health = 1;


    private Rigidbody2D _rigiddody2D;
    private Animator _animator;
    
    void Awake(){
        _rigiddody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartCoroutine(TimeWhenSelfDestroy());
    }

    private void Update(){
        DoMove();
    }

    private void SetTarget(){
        if(_isEnemyMissile)
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        else
            _target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }


    public override void DoMove(){
        if (_target == null)
            SetTarget();
        try
        {
            Vector2 direction = (Vector2)_target.position - _rigiddody2D.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            _rigiddody2D.angularVelocity = -rotateAmount * _rotateSpeed;
            _rigiddody2D.velocity = transform.up * _speed;
        }
        catch
        {

        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable == null) return;
        if(_isEnemyMissile && collider.tag =="Player"){
            damageable.Damege(_damage);
            Dead();
        }
        if(!_isEnemyMissile && collider.tag =="Enemy"){
            damageable.Damege(_damage);
            Dead();
        }
    }

    
    protected override void Dead(){
        _animator.SetTrigger("Dead");
        Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);

    }

    
    private IEnumerator TimeWhenSelfDestroy(){
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    public void Damege(int damage){
        Debug.Log(_health);
        _health -= damage;
        if(_health <0)
            Dead();
    }
}
