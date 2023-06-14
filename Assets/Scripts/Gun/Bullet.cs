using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


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
        //transform.position += new Vector3(0, -1, 0) * _bulletSpeed * Time.deltaTime; 
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            Dead();
    }

    private void Dead()
    {
        if (transform.parent != null & !_isEnemyShoot)
        {
            Debug.Log("Test");
           if (gameObject.transform.parent.transform.childCount == 1 )
                Destroy(transform.parent.gameObject);

        }

        Destroy(gameObject);
    }

}
