using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(AudioSource),typeof(Animator))]
public class ShipEnemy : Enemy
{
    [Header("Enemy Settings")]
    [SerializeField] private int _health=3;

    [SerializeField] private float _speed=4;

    [SerializeField] private int _damage=1;

    [SerializeField] private int _score = 1;


    [Header("PowerUp")]
    [RangeAttribute(0,1f)]
    [SerializeField] private float _chanceSpawnTripleShotPowerUp;

    [SerializeField] private GameObject[] _powerUps;

  

    [Header("Soot")]
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speedBullet;


    [Header("Dead")]
    [SerializeField] private AudioClip _explosionSound;

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
    public override void Damege(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            Dead();
    }

    protected override void DoShoot()
    {
       StartCoroutine(ShootRoutine());
    }

    
    IEnumerator ShootRoutine(){
        while (true)
        {
            GameObject bullet;
            bullet = Instantiate(_bullet, new Vector3(transform.position.x,transform.position.y - 2.1f, 0),Quaternion.identity,transform);
            bullet.GetComponent<Bullet>().SetDamage(_damage);
            yield return new WaitForSeconds(_fireRate);
        }
       
    }

    protected override void Dead()
    {
       
        StopAllCoroutines();

        _animator.SetTrigger("IsEnemyDead");
        this.GetComponent<BoxCollider2D>().enabled = false;

        if (_chanceSpawnTripleShotPowerUp >= Random.Range(0, 1.0f))
            Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
        transform.parent.GetComponent<SpawnManager>().KilledEnemy(_score,this.gameObject);

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        Destroy(gameObject, 2.8f);
    }


}
