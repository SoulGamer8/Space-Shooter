using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate;

    private bool _isShooting = false;
    private float _canFire = -1f;


    
    private void Shooting()
    {
        
    }

    public void FireBullet()
    {
        if(Time.time > _canFire)
        {
            Instantiate(_bullet, new Vector3(transform.position.x, transform.position.y + 1.2f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
      

        Debug.Log("Start");
        _isShooting = true;
        Shooting();
    }

    public void StopFire()
    {
        Debug.Log("Stop");
        _isShooting = false;
    }
}
