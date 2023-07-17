using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed=3;

    [SerializeField] private bool _isEnemyShoot =false;

    private bool _trippleShootIsActive;
    private float _trippleShootSpeed;

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

        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (_trippleShootIsActive)
        {
            transform.position += new Vector3(_trippleShootSpeed, _bulletSpeed, 0) * Time.deltaTime;
        }


        if (transform.position.y > 10 || transform.position.y < -10)
        {
            if (transform.parent != null && !_isEnemyShoot)
                Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    public void Change(float isRight)
    {
        _trippleShootIsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && !_isEnemyShoot)
            Dead();
        if (collider.tag == "Player" && _isEnemyShoot)
        {
            collider.GetComponent<PlayerHealth>().TakeDamage(1);
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
