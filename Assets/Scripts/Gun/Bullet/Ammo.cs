using UnityEngine;

public abstract class Ammo : MonoBehaviour, IMoveable
{
    public virtual void DoMove(){}

    protected abstract void Dead();
    protected abstract void OnTriggerEnter2D(Collider2D collider);

}
