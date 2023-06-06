using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed=3;

    private bool _trippleShootIsActive;
    private float _trippleShootSpeed;

    private void Start()
    {
        _trippleShootSpeed = _bulletSpeed;
        _trippleShootIsActive = false;
    }

    void Update()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (_trippleShootIsActive)
        {
            transform.position += new Vector3(_trippleShootSpeed, _bulletSpeed, 0) * Time.deltaTime;
        }


        if (transform.position.y > 10)
        {
            if (transform.parent != null)
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
        if (transform.parent != null)
        {
           if (gameObject.transform.parent.transform.childCount == 1 )
                Destroy(transform.parent.gameObject);

        }

        Destroy(gameObject);
    }

}
