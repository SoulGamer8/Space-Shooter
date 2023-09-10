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
    [SerializeField] private float _fireRate;

    [Header("Sound")]
    [SerializeField] private AudioClip _laserShoot;

    [Header("Rocket")]
    [SerializeField] private GameObject _rocket;
    [SerializeField] private List<GameObject> _ammoUI;
    private  AmmoMissile _ammoMissile;
    
    private int _volleyLaserSpread = 20;
    private int _countLaserToFire  =3;
    [SerializeField] private bool _isTripleShootActive = false;
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
            fire = StartCoroutine(ShootingCoroutine());
        if (value.canceled)
            StopCoroutine(fire);

    }

    public void TakeTripleShoot(){
        _isTripleShootActive = true;
        StartCoroutine(ActivePowerUpCoroutine());
    }

    public void TakeAmmo(){
        _ammoMissile.AddMissile();
    }
    public void FireRocket(InputAction.CallbackContext value){
        if (value.started &&  _ammoMissile.UseMissile())
            Instantiate(_rocket, new Vector3(transform.position.x - 0.2f, transform.position.y + 1.0f, 0), Quaternion.identity);
    }

    private IEnumerator ActivePowerUpCoroutine(){
        yield return new WaitForSeconds(5.0f);
        _isTripleShootActive = false;

    }

    private void TripleShoot(){
         int angelBetweenLaser = 2 * _volleyLaserSpread / (_countLaserToFire-1);

        for(int angle = -_volleyLaserSpread;angle<= _volleyLaserSpread;angle += angelBetweenLaser){
            GameObject.Instantiate(_bullet,transform.position,Quaternion.Euler(0,0,angle));  
        }
    }

    private IEnumerator ShootingCoroutine(){
        while (true) 
        {
            if (_isTripleShootActive)
            {
                TripleShoot();
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
