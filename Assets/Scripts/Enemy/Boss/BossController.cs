using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    [SerializeField] internal float _bossSpeed;


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
    
    #endregion

    #region Beam
    [Header("Beam")]
    [SerializeField] internal BeamObject[] _beamObject;
    [SerializeField] internal float _beamAttackTime;
    [SerializeField] internal float _trackingDuration;
    [SerializeField] internal float _timetoLock;
    [SerializeField] internal float _trackingFlashTime;
    [SerializeField] internal float _lockedFlashTime ;
    internal Transform _playerTransform;
    
    #endregion

    #region Missile State
    [Header("Missile")]
    [SerializeField] internal GameObject _missile;
    [SerializeField] internal float _fireRateMissile;
    [SerializeField] internal int _amountMissilesVolley;
    [SerializeField] internal Transform[] _spawnMissile;
    #endregion

    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;

    #region State Machine
    BossStateMachine bossStateMachine;

    public SpawnState spawnState;
    public ShootingLaserState shootLaserState;
    public MissileState shootMissileState;
    public BeamState beamState;

    #endregion

    private void Awake() {
        bossStateMachine = new BossStateMachine();

        spawnState = new SpawnState(this,bossStateMachine);
        shootLaserState = new ShootingLaserState(this,bossStateMachine);
        shootMissileState = new MissileState(this,bossStateMachine);
        beamState = new BeamState(this,bossStateMachine);

        _curentlyHealth = _healthMax;
        GameObject healthBarUI;
        healthBarUI = GameObject.FindGameObjectWithTag("HealthBar");
        
        _healthBar = Instantiate(_healthBarPrefab,healthBarUI.transform).GetComponent<BossHealthBar>();
        _healthBar.SetValueMax(_healthMax);
    }

    private void Start() {
        bossStateMachine.Initialize(spawnState);
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        bossStateMachine.currentEnemyState.UpdateState();
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
