using UnityEngine;

public class AsteroidEnemy : Enemy
{
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

    protected override void DoMove(){
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;
        if (transform.position.y < -5)
        {
            Dead();
        }
    }

    public override void Damege(int damage){
        base.Damege(damage);
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
        _animator.SetTrigger("IsEnemyDead");
        this.GetComponent<BoxCollider2D>().enabled = false;

        _audioSource.clip = _explosionSound;
        _audioSource.Play();

        Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
    }



    protected override void DoShoot(){
    }

    public override int GetSpawnChanceWeight(){
       return _spawnChanceWeight;
    }
}
