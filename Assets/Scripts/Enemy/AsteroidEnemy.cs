using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy
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

  


    [Header("Dead")]
    [SerializeField] private AudioClip _explosionSound;

    private AudioSource _audioSource;

    private Animator _animator;

    private void Awake(){
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360)); 
    }

    private void Update(){
        DoMove();
    }

    protected override void DoMove()
    {
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;
        if (transform.position.y < -5)
        {
            Dead();
        }
    }

    public override void Damege(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            Dead();
    }

    protected override void Dead()
    {
        _animator.SetTrigger("IsEnemyDead");
        this.GetComponent<BoxCollider2D>().enabled = false;

        if (_chanceSpawnTripleShotPowerUp >= Random.Range(0, 1.0f))
            Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
        transform.parent.GetComponent<SpawnManager>().KilledEnemy(_score,this.gameObject);

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void DoShoot()
    {
    }
}
