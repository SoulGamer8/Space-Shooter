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

    [SerializeField] private int _damage;

    private Rigidbody2D _rigiddody2D;
    private SpawnManager _spawnManager;

    private Coroutine _timeWhenMissileSelfDestroy;
    void Awake()
    {
        _rigiddody2D = GetComponent<Rigidbody2D>();
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    public int GetDamage()
    {
        return _damage;
    }

    private void Update()
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
        if (target == null)
            _timeWhenMissileSelfDestroy = StartCoroutine(TimeWhenSelfDestroy());
        else
        {
            _target = target.transform;
            if(_timeWhenMissileSelfDestroy  != null)
                StopCoroutine(_timeWhenMissileSelfDestroy);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator TimeWhenSelfDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
