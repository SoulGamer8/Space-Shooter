using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : Ammo
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 200f;

    [SerializeField] private int _damage;

    private Rigidbody2D _rigiddody2D;
    private Coroutine _timeWhenMissileSelfDestroy;
    
    void Awake()
    {
        _rigiddody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(TimeWhenSelfDestroy());
    }

    private void Update()
    {
        DoMove();
    }

    public void SetTarget()
    {
        _target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }


    protected override void DoMove()
    {
        if (_target == null)
            SetTarget();
        try
        {
            Vector2 direction = (Vector2)_target.position - _rigiddody2D.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            _rigiddody2D.angularVelocity = -rotateAmount * _rotateSpeed;
            _rigiddody2D.velocity = transform.up * _speed;
        }
        catch
        {

        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null  && collider.tag =="Enemy")
        {
            damageable.Damege(_damage);
            Dead();
        }
    }

    
    protected override void Dead()
    {
        Destroy(gameObject);
    }

    
    private IEnumerator TimeWhenSelfDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }


}
