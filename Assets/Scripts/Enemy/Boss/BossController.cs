using System.Collections;
using UnityEngine;

namespace Boss{
    public class BossController : MonoBehaviour
    {
        [SerializeField] internal bool _isSecondStage= false;
        [SerializeField] internal float _bossSpeed;

        
        #region SpawnState
        [SerializeField] internal Vector3 _target; 
        #endregion
        
        #region Shooting Laser State
        [Header("Laser")]

        [SerializeField] internal GameObject _laser;
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
        [SerializeField] internal Transform[] _gunMissile;
        #endregion

        #region Shield State
        [Header("Shield")]
        [SerializeField] internal GameObject _shield;
        [SerializeField] internal GameObject _shieldGenerator;
        [SerializeField] internal float _weekShieldOpacity;
        internal bool _isShieldActive = false;

        #endregion

        #region Smart Laser State
        [Header("Smart Laser")]
        [SerializeField] internal GameObject _smartLaser;
        [SerializeField] internal GameObject[] _gunLaser;
        [SerializeField] internal float _fireRateSmartLaser;
        #endregion

        #region State Machine
        public BossStateMachine bossStateMachine;

        public SpawnState spawnState;
        public ShootingLaserState shootLaserState;
        public MissileState missileState;
        public BeamState beamState;
        public ShieldState shieldState;
        public IdleState idleState;
        public SmartLaserState smartLaserState;

        #endregion
        
        internal  Coroutine _changeStateToMissleStateRoutine;
        
        internal BossHealth bossHealth;
        internal BossMove _bossMove;
        private void Awake() {
            bossStateMachine = new BossStateMachine();

            spawnState = new SpawnState(this,bossStateMachine);
            shootLaserState = new ShootingLaserState(this,bossStateMachine);
            missileState = new MissileState(this,bossStateMachine);
            beamState = new BeamState(this,bossStateMachine);
            shieldState = new ShieldState(this,bossStateMachine);
            idleState = new IdleState(this,bossStateMachine);
            smartLaserState = new SmartLaserState(this,bossStateMachine);

            bossHealth = GetComponent<BossHealth>();
            _bossMove =  GetComponent<BossMove>();
        }

        private void Start() {
            bossStateMachine.Initialize(spawnState);
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update() {
            bossStateMachine.currentEnemyState.UpdateState();
        }

        public void ShieldGeneratorDead(){
            shieldState.ShieldGeneratorDestroy();
        }

        public void StartFirstStage(){
            
        }

        public void NextState(){
            if(_isSecondStage){
                bossStateMachine.ChangeState(smartLaserState);
                _changeStateToMissleStateRoutine = StartCoroutine(ChangeStateToMissleStateRoutine(5));
            }
            else{
                bossStateMachine.ChangeState(shootLaserState);
                _changeStateToMissleStateRoutine = StartCoroutine(ChangeStateToMissleStateRoutine(5));
            }
        }

        private IEnumerator ChangeStateToMissleStateRoutine(float time){
            Debug.Log("Start");
            yield return new WaitForSeconds(time);
            bossStateMachine.ChangeState(missileState);
        }

        private IEnumerator SpawnShield(){
            yield return new WaitForSeconds(10);
            bossStateMachine.ChangeState(shieldState);
        }


    }
}