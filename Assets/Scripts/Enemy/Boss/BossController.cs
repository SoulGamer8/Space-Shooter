using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    
    #region Health
    [Header("Health")]
    [SerializeField] private int _healthMax;
    [SerializeField] private GameObject _healthBarPrefab;
    private BossHealthBar _healthBar;
    private int _curentlyHealth;
    #endregion

    #region  Stages percent start
    [Header("Stages percent start")]
    [Range(0,100)]
    [SerializeField] private int _percentStartStage2;
    #endregion
    
    #region SpawnState
    [SerializeField] internal Vector3 _target; 
    #endregion
    
    #region Shooting Laser State
    [Header("Bullet")]
    [SerializeField] internal GameObject _bullet;
    [SerializeField] internal float _fireRateBullet;  
    [SerializeField] internal int _countLaserToFire;
    [SerializeField] internal int _volleyLaserSpread;
    [SerializeField] internal float _bossSpeed;

    #endregion

    [Header("Missile")]
    [SerializeField] private GameObject _missile;
    [SerializeField] private float _fireRateMissile;

    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;

    #region State Machine
    BossStateMachine bossStateMachine;

    SpawnState spawnState;
    ShootingLaserState shootLaserState;
    MissileState shootMissileState;
    #endregion



    private void Awake() {
        bossStateMachine = new BossStateMachine();

        spawnState = new SpawnState(this,bossStateMachine);
        shootLaserState = new ShootingLaserState(this,bossStateMachine);
        shootMissileState = new MissileState(this,bossStateMachine);


        _curentlyHealth = _healthMax;
        GameObject healthBarUI;
        healthBarUI = GameObject.FindGameObjectWithTag("HealthBar");
        _healthBar = Instantiate(_healthBarPrefab,healthBarUI.transform).GetComponent<BossHealthBar>();
        _healthBar.SetValueMax(_healthMax);
    }


    private void Start() {
        bossStateMachine.Initialize(spawnState);
        
    }

    private void Update() {
        bossStateMachine.currentEnemyState.UpdateState();
    }

    public void NextStage(){
        bossStateMachine.ChangeState(shootLaserState);
    }

    #region Damage

    public void Damege(int damage)
    {
        _curentlyHealth -= damage;

        if(_curentlyHealth < (_percentStartStage2*_healthMax)/100)
           Debug.Log("Stage 2");
        if (_curentlyHealth < 0)
            Dead();
        UpdateHealthBar();
    }

    
    private void UpdateHealthBar(){
        _healthBar.UpdateUI(_curentlyHealth);
    }

    #endregion

    private void Dead(){
        Destroy(this.gameObject);
    }
}