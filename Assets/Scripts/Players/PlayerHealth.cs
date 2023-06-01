using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;


    private void Dead()
    {
        Destroy(this); 
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <=0)
        {
            Dead();
        }
        Debug.Log(_health);
    }
}
