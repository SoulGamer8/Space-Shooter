using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(AudioSource),typeof(Animator))]
public class ShipEnemy : Enemy
{
    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private int _bulletSpeed;
    [SerializeField] private float _fireRate;



    private AudioSource _audioSource;

    private Animator _animator;


    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        DoShoot();
    }

    private void Update(){
      DoMove();
    }

    
    protected override void DoMove(){
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;
        if (transform.position.y < -5)
        {
            transform.parent.GetComponent<SpawnManager>().KilledEnemy(0, this.gameObject);
            Destroy(gameObject);
        }
    }
    public override void Damege(int damage){
       base.Damege(damage);
    }

    protected override void DoShoot(){
       StartCoroutine(ShootRoutine());
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void Dead(){
       
        StopAllCoroutines();

        _animator.SetTrigger("IsEnemyDead");
        this.GetComponent<BoxCollider2D>().enabled = false;

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
    }

    
    IEnumerator ShootRoutine(){
        while (true)
        {
            GameObject bullet;
            bullet = Instantiate(_bullet, new Vector3(transform.position.x,transform.position.y - 2.1f, 0),Quaternion.identity,transform);
            bullet.GetComponent<EnemyLaser>().SetDamageAndSpeed(_bulletDamage,_bulletSpeed);
            yield return new WaitForSeconds(_fireRate);
        }
       
    }

    
}
