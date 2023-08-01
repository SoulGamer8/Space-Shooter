using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateController : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRateBullet;
    [Header("Missile")]
    [SerializeField] private GameObject _missile;
    [SerializeField] private float _fireRateMissile;
    
    [Header("Mine")]
    [SerializeField] private GameObject _mine;
    [SerializeField] private float _spawnRateMine;

    [Header("Enemy")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _spawnRateEnemy;

    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;

     private float _fireRate = 2f;
     
     private float timer = 0.0f;

    State currentState;
    ShootBulletState shootBulletState = new ShootBulletState();
    ShootRocketState shootRocketState = new ShootRocketState();
    SpawnBombsState spawnBombsState = new SpawnBombsState();
    SpawnEnemyState spawnEnemyState = new SpawnEnemyState();

    private void Start()
    {
        currentState = shootBulletState;
         currentState.OnEnter(_bullet,_fireRateBullet);
    }
    void Update()
    {
        currentState.UpdateState();

        timer += Time.deltaTime;

        if (timer > _fireRate)
        {
            Debug.Log("UpdateState");


            timer = timer - _fireRate;
        }
    }

    public void ChangeState(State newState,GameObject gameObject,float fireRate)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter( gameObject,fireRate);
    }

    public void FirstPhase(){
        ChangeState(shootBulletState,_bullet,_fireRateBullet);
    }

    public void SecondPhase(){
        ChangeState(shootRocketState,_missile,_fireRateMissile);
    }

    public void ThirdPhase(){
        ChangeState(spawnEnemyState,_enemy,_fireRateBullet);
    }
}
