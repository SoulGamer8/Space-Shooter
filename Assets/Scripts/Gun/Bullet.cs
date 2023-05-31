using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed=10;

    void Update()
    {
        transform.position += new Vector3(0, _bulletSpeed, 0) *Time.deltaTime ;


        if (transform.position.y > 10)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void Desttoy()
    {
        Destroy(gameObject);
    }
}
