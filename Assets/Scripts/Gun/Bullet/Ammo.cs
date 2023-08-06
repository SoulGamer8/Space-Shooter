using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    protected abstract void DoMove();
    protected abstract void Dead();
    protected abstract void OnTriggerEnter2D(Collider2D collider);
}
