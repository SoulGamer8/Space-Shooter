using UnityEngine;


public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    [SerializeField] protected int _health=3;

    [SerializeField] protected float _speed=4;

    [SerializeField] protected int _damage=1;

    [SerializeField] protected int _score = 1;

    [Header("Dead")]
    [SerializeField] protected AudioClip _explosionSound;




    protected abstract void DoMove();

    protected abstract void DoShoot();

    public virtual void Damege(int damage){
        _health -= damage;
        if(_health < 0)
            Dead();
    }

    protected abstract void OnTriggerEnter2D(Collider2D collider);
    
    protected abstract void Dead();


}
