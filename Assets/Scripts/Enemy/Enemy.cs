using UnityEngine;


public abstract class Enemy : MonoBehaviour, IDamageable, ISpawnChanceWeight
{
    [Header("Enemy Settings")]
    [SerializeField] protected int _health=3;
    [SerializeField] protected float _speed=4;
    [SerializeField] protected int _damage=1;
    [SerializeField] protected int _score = 1;
    [SerializeField] protected int _spawnChanceWeight;

    [Header("Dead")]
    [SerializeField] protected AudioClip _explosionSound;

    public abstract int GetSpawnChanceWeight();

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
