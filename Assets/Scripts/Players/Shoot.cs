using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public event UnityAction<AudioClip> PlaySoundSoot;

    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _tripleShoot;
    [SerializeField] private float _fireRate;

    [Header("Sound")]
    [SerializeField] private AudioClip _laserShoot;

    [Header("Rocket")]
    [SerializeField] private GameObject _rocket;
    [SerializeField] private List<GameObject> _ammoUI;
    private  AmmoMissile _ammoMissile;
    
    private bool _isTripleShootActive = false;
    private Coroutine fire;

    private void Awake(){
       SpawnAmmoUI();
    }

    private void SpawnAmmoUI(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject canvas = GameObject.FindGameObjectWithTag("HealthBar");

        GameObject missileUI;

        missileUI = Instantiate(_ammoUI[players.Length-1],canvas.transform);
        _ammoMissile = missileUI.GetComponent<AmmoMissile>();

    }

    public void FireBullet(InputAction.CallbackContext value){
       
        if (value.started)
            fire = StartCoroutine(Shooting());
        if (value.canceled)
            StopCoroutine(fire);

    }

    public void TakeTripelShoot(){
        _isTripleShootActive = true;
        StartCoroutine(ActivePowerUp());
    }

    public void TakeAmmo(){
        _ammoMissile.AddMissile();
    }
    public void FireRocket(InputAction.CallbackContext value){
        if (value.started &&  _ammoMissile.UseMissile())
            Instantiate(_rocket, new Vector3(transform.position.x - 0.2f, transform.position.y + 1.0f, 0), Quaternion.identity);
    }

    private IEnumerator ActivePowerUp(){
        yield return new WaitForSeconds(5.0f);
        _isTripleShootActive = false;

    }

    private IEnumerator Shooting(){
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
                GameObject bullet;
                bullet = Instantiate(_bullet, new Vector3(transform.position.x, transform.position.y + 1.5f, 0), Quaternion.identity);
            }

            PlaySoundSoot?.Invoke(_laserShoot);

            yield return new WaitForSeconds(_fireRate);
        }

    }
}
