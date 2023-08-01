using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Enemy : MonoBehaviour, IDamageable
{

    protected abstract void DoMove();

    protected abstract void DoShoot();

    public abstract void Damege(int damage);
    
    protected abstract void Dead();


}
