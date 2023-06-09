using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private int _health=3;

    [SerializeField] private float _speed=4;

    [SerializeField] private int _damage=1;

    [SerializeField] private int _score = 1;

    [RangeAttribute(0,1f)]
    [SerializeField] private float _chanceSpawnTripleShotPowerUp;

    [SerializeField] private GameObject[] _powerUps;

    [SerializeField] private Score score;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime); 

        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _health--;
            if (_health <= 0)
            {
                Dead();
                Destroy(gameObject);
            }
        }
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void Dead()
    {
        if (_chanceSpawnTripleShotPowerUp >= Random.Range(0, 1.0f))
            Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        transform.parent.GetComponent<SpawnManager>().KilledEnemy(_score);
    }

}
