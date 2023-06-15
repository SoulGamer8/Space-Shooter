using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Shoot : MonoBehaviour
{
    public event UnityAction<AudioClip> PlaySoundSoot;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _tripleShoot;

    [SerializeField] private float _fireRate;

    [Header("Sound")]
    [SerializeField] private AudioClip _laserShoot;


    private bool _isTripleShootActive = false;
    private float _canFire = -1f;

  
    public void FireBullet()
    {
        if(Time.time > _canFire)
        {
            if (_isTripleShootActive)
            {
                Instantiate(_tripleShoot, new Vector3(transform.position.x - 0.2f, transform.position.y + 4.0f, 0), Quaternion.identity);
                _tripleShoot.transform.GetChild(1).gameObject.GetComponent<Bullet>().Change(10);
                _tripleShoot.transform.GetChild(2).gameObject.GetComponent<Bullet>().Change(-10);
            }
            else
            {
                Instantiate(_bullet, new Vector3(transform.position.x, transform.position.y + 1.5f, 0), Quaternion.identity);
            }

            PlaySoundSoot?.Invoke(_laserShoot);
          
            _canFire = Time.time + _fireRate;
        }
      
    }

    public void TakePowerUp()
    {
        _isTripleShootActive = true;
        StartCoroutine(ActivePowerUp());
    }




    private IEnumerator ActivePowerUp()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShootActive = false;

    }
}
