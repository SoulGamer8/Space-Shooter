using System.Collections;
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

    #region Stages percent start
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

    #region Beam State
    [Header("Beam")]
    [SerializeField] internal BeamObject[] _beamObject;
    [SerializeField] internal float _beamAttackTime;
    [SerializeField] internal float _trackingDuration;
    [SerializeField] internal float _timetoLock;
    [SerializeField] internal float _trackingFlashTime;
    [SerializeField] internal float _lockedFlashTime;
    [SerializeField] internal int _amountBeamVolley;
    internal Transform _playerTransform;
    
    #endregion

    #region Missile State
    [Header("Missile")]
    [SerializeField] internal GameObject _missile;
    [SerializeField] internal float _fireRateMissile;
    [SerializeField] internal int _amountMissilesVolley;
    [SerializeField] internal Transform[] _spawnMissile;
    #endregion

    #region Shield
    [Header("Shield")]
    [SerializeField] internal GameObject _shield;
    [SerializeField] internal GameObject _shieldGenerator;
    [SerializeField] internal float _weekShieldOpacity;
    internal bool _isShieldActive = false;

    #endregion

    #region State Machine
    BossStateMachine bossStateMachine;

    public SpawnState spawnState;
    public ShootingLaserState shootLaserState;
    public MissileState missileState;
    public BeamState beamState;
    public ShieldState shieldState;
    IdleState idleState;

    #endregion

    #region Second Stage
    [Header("Second Stage")]
    [SerializeField] private float _secondPhaseExplosionDuration;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _miniExplosionFrequency;
    [SerializeField] private Sprite _secondPhaseSprite;
    [SerializeField] private GameObject _phaseOneHitboxes;
    [SerializeField] private Transform _phaseExplosionPoints;
    [SerializeField] private float _miniExposionScale;
    [SerializeField] private bool _isSecondStage= false;

    #endregion
    
    private Coroutine _changeStateToMissleStateRoutine;

    private CameraManager _cameraManager;
    

    private void Awake() {
        bossStateMachine = new BossStateMachine();

        spawnState = new SpawnState(this,bossStateMachine);
        shootLaserState = new ShootingLaserState(this,bossStateMachine);
        missileState = new MissileState(this,bossStateMachine);
        beamState = new BeamState(this,bossStateMachine);
        shieldState = new ShieldState(this,bossStateMachine);
        idleState = new IdleState(this,bossStateMachine);

        _curentlyHealth = _healthMax;
        GameObject healthBarUI;
        healthBarUI = GameObject.FindGameObjectWithTag("HealthBar");
        
        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();

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
        if(!_isShieldActive)
            _curentlyHealth -= damage;

        if(_curentlyHealth < (_percentStartStage2*_healthMax)/100 && !_isSecondStage)
           StartCoroutine(OnHalfHPRoutine());
        if (_curentlyHealth < 0)
            Dead();
        UpdateHealthBar();
    }

    
    private void UpdateHealthBar(){
        _healthBar.UpdateUI(_curentlyHealth);
    }

    #endregion


    public void ShieldGeneratorDead(){
        shieldState.ShieldGeneratorDestroy();
    }

    private void Dead(){
        Destroy(this.gameObject);
    }


    public void StartFirstStage(){
        _changeStateToMissleStateRoutine = StartCoroutine(ChangeStateToMissleStateRoutine());
    }

    private IEnumerator ChangeStateToMissleStateRoutine(){
        yield return new WaitForSeconds(5);
        bossStateMachine.ChangeState(missileState);


    }

    IEnumerator OnHalfHPRoutine(){
        _isShieldActive = true;
        bossStateMachine.ChangeState(idleState);
        _isSecondStage = true;

        // _cameraManager.CameraShake(0.1f, _secondPhaseExplosionDuration);
        yield return MiniExplosionsRoutine(_phaseOneHitboxes, _secondPhaseExplosionDuration);

        // big explosions
        for (int index = 0; index < _phaseExplosionPoints.childCount; index++)
        {
            Vector3 explodePosition = _phaseExplosionPoints.GetChild(index).position;
            Instantiate(_explosion, explodePosition, Quaternion.identity);
        }
        //change sprite after a short time
        yield return new WaitForSeconds(0.3f);
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sprite = _secondPhaseSprite;

        bossStateMachine.ChangeState(shieldState);
        StopCoroutine(_changeStateToMissleStateRoutine);
    }

    private IEnumerator MiniExplosionsRoutine(GameObject hitBoxesExplode,float duration){
        float timer = 0;
        float nextExplosionTime = 0;
        float timeBetweenExplosion = 1 / _miniExplosionFrequency;
        while(timer < duration){
            if(timer>=nextExplosionTime){
                MiniExplosions(hitBoxesExplode);
                nextExplosionTime +=timeBetweenExplosion;
            }
            yield return null;
            timer +=Time.deltaTime;
        }

    }

    private void MiniExplosions(GameObject hitBoxsParent){
        Vector3 explodeScale = new Vector3(_miniExposionScale,_miniExposionScale,1);
        int hitboxCount = hitBoxsParent.transform.childCount;
        for(int i = 0; i< hitboxCount;i++){
            Collider2D hitbox = hitBoxsParent.transform.GetChild(i).GetComponent<Collider2D>();
            Bounds hitboxArea = hitbox.bounds;
            Vector3 randomPoint = new Vector3(
                Random.Range(hitboxArea.min.x,hitboxArea.max.x),
                Random.Range(hitboxArea.min.y,hitboxArea.max.y), 0);
            GameObject newExplosion = Instantiate(_explosion,randomPoint,Quaternion.identity);
            newExplosion.transform.localScale = explodeScale;
        }
    }

}
