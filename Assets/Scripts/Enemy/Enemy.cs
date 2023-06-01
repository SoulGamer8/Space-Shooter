using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health=3;

    [SerializeField] private float _speed=4;

    [SerializeField] private int _damage=1;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime); 

        if(transform.position.y > 5)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            _health--;
            if (_health <= 0)
                Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
                playerHealth.TakeDamage(_damage);
        }
    }

}
