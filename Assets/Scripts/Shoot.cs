using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private bool _isShooting = false;


    
    private void Shooting()
    {
        while (_isShooting)
        {
            Debug.Log("FIRE");
        }
    }

    public void Fire()
    {
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
