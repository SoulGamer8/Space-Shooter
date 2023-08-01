using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed=3;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private bool _isEnemyShoot =false;


    private bool _trippleShootIsActive;
    private float _trippleShootSpeed;

    public void SetDamage(int damage)
    {
        if(damage >0)
            _bulletDamage = damage;
    }

    private void Start()
    {
        _trippleShootSpeed = _bulletSpeed;
        _trippleShootIsActive = false;
        if (_isEnemyShoot)
        {
            _bulletSpeed *= -1;
        }
    }

    void Update()
    {
        DoMove();
    }


    private void DoMove(){
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (_trippleShootIsActive)
        {
            transform.position += new Vector3(_trippleShootSpeed, _bulletSpeed, 0) * Time.deltaTime;
        }

        if (transform.position.y > 10 || transform.position.y < -10)
            Dead();
    }

    public void Change(float isRight)
    {
        _trippleShootIsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null && !_isEnemyShoot && collider.tag !="Player")
        {
            damageable.Damege(_bulletDamage);
            Dead();
        }
        else if(damageable != null && _isEnemyShoot){
            damageable.Damege(_bulletDamage);
            Dead();
        }
    }


    private void Dead()
    {
        if (transform.parent != null && !_isEnemyShoot)
        {
           if (gameObject.transform.parent.transform.childCount == 1 )
                Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

}
