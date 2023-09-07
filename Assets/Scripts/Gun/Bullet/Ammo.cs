using UnityEngine;

public abstract class Ammo : MonoBehaviour, IMoveable
{
    [SerializeField] protected int _bulletSpeed=8;
    [SerializeField] protected int _bulletDamage=1;


    public virtual void DoMove(){ 
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (transform.position.y > 10 || transform.position.y < -10)
           Dead();
    }

    protected virtual void Dead(){
        Destroy(gameObject);
    }
    protected abstract void OnTriggerEnter2D(Collider2D collider);

}
