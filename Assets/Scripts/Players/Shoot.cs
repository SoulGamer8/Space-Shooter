using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public event UnityAction<AudioClip> PlaySoundSoot;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _tripleShoot;

    [SerializeField] private float _fireRate;

    [Header("Sound")]
    [SerializeField] private AudioClip _laserShoot;


    [SerializeField] private GameObject _rocket;
    private bool _isTripleShootActive = false;


    private Coroutine fire;


    public void FireBullet(InputAction.CallbackContext value)
    {
       
        if (value.started)
            fire = StartCoroutine(Shooting());
        if (value.canceled)
            StopCoroutine(fire);

    }

    public void TakePowerUp()
    {
        _isTripleShootActive = true;
        StartCoroutine(ActivePowerUp());
    }

    public void FireRocket(InputAction.CallbackContext value)
    {
        if (value.started)
           Instantiate(_rocket, new Vector3(transform.position.x - 0.2f, transform.position.y + 1.0f, 0), Quaternion.identity);
       
    }



    private IEnumerator ActivePowerUp()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShootActive = false;

    }

    private IEnumerator Shooting()
    {
        while (true) 
        {
            if (_isTripleShootActive)
            {
                Instantiate(_tripleShoot, new Vector3(transform.position.x - 0.2f, transform.position.y + 4.0f, 0), Quaternion.identity);
                _tripleShoot.transform.GetChild(1).gameObject.GetComponent<PlayerLaser>().ActiveTrippleShot();
                _tripleShoot.transform.GetChild(2).gameObject.GetComponent<PlayerLaser>().ActiveTrippleShot();
            }
            else
            {
                Instantiate(_bullet, new Vector3(transform.position.x, transform.position.y + 1.5f, 0), Quaternion.identity);
            }

            PlaySoundSoot?.Invoke(_laserShoot);

            yield return new WaitForSeconds(_fireRate);
        }

    }
}
