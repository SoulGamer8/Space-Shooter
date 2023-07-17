using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 200f;

    [SerializeField] private int _damage = 3;

    private Rigidbody2D _rigiddody2D;
    private SpawnManager _spawnManager;
    void Awake()
    {
        _rigiddody2D = GetComponent<Rigidbody2D>();
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }


    private void FixedUpdate()
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

    public void SetTarget()
    {
        GameObject target = _spawnManager.GetRandomObject();
        _target = target.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
