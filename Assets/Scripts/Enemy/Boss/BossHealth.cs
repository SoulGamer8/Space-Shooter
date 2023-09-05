
using System.Collections;
using UnityEngine;

namespace Boss{
    public class BossHealth : MonoBehaviour, IDamageable
    {
        #region Health
        [Header("Health")]
        [SerializeField] private int _healthMax;
        [SerializeField] private GameObject _healthBarPrefab;
        private BossHealthBar _healthBar;
        internal int _curentlyHealth;
        #endregion

        #region Stages percent start
        [Header("Stages percent start")]
        [Range(0,100)]
        [SerializeField] private int _percentStartStage2;
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
        #endregion
        
        #region Death
        [Header("Death")]
        [SerializeField] private float _deathExplosionDuration;
        [SerializeField] private GameObject _phaseTwoHitboxes;
        [SerializeField] private float _deathExplosionScale;
        #endregion

        private CameraManager _cameraManager;
        private BossController bossController;

        private void Awake() {
            _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
            bossController = GetComponent<BossController>();

            _curentlyHealth = _healthMax;
            GameObject healthBarUI;
            healthBarUI = GameObject.FindGameObjectWithTag("HealthBar");
            
            _healthBar = Instantiate(_healthBarPrefab,healthBarUI.transform).GetComponent<BossHealthBar>();
            _healthBar.SetValueMax(_healthMax);
        }

        public void Damege(int damage){
            if(!bossController._isShieldActive)
                _curentlyHealth -= damage;
            if(_curentlyHealth < _percentStartStage2*_healthMax/100 && !bossController._isSecondStage)
                StartCoroutine(SecondStageCoroutine());
            else if (_curentlyHealth <= 0)
                StartCoroutine(DeathEffectsCoroutine());
            UpdateHealthBar();
        }

        public void UpdateHealthBar(){
            _healthBar.UpdateUI(_curentlyHealth);
        }
        
        private IEnumerator SecondStageCoroutine(){
            bossController. _isShieldActive = true;
            bossController._isSecondStage = true;
            bossController.bossStateMachine.ChangeState(bossController.idleState);

            _cameraManager.CameraShake(_secondPhaseExplosionDuration, 0.1f,false);
            yield return MiniExplosionsCoroutine(_phaseOneHitboxes, _secondPhaseExplosionDuration);

            for (int index = 0; index < _phaseExplosionPoints.childCount; index++){
                Vector3 explosionPosition = _phaseExplosionPoints.GetChild(index).position;
                Instantiate(_explosion, explosionPosition, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(0.3f);
            SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sprite = _secondPhaseSprite;

            bossController.bossStateMachine.ChangeState(bossController.shieldState);
            StopCoroutine(bossController._changeStateToMissleStateRoutine);
        }

        private IEnumerator DeathEffectsCoroutine(){
            _cameraManager.CameraShake(_deathExplosionDuration, 0.1f,false);
            yield return MiniExplosionsCoroutine(_phaseTwoHitboxes, _deathExplosionDuration);

            GameObject explosion = Instantiate(_explosion, this.transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(_deathExplosionScale, _deathExplosionScale, 1);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().LevelComplete();

            Destroy(this.gameObject, 0.3f);
        }

        private IEnumerator MiniExplosionsCoroutine(GameObject hitBoxesExplode,float duration){
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
}