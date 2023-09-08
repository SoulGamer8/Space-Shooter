
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
        internal int _currentlyHealth;
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
        [SerializeField] private GameObject _phaseOneHitBoxes;
        [SerializeField] private Transform _phaseExplosionPoints;
        [SerializeField] private float _miniExplosionScale;
        #endregion
        
        #region Death
        [Header("Death")]
        [SerializeField] private float _deathExplosionDuration;
        [SerializeField] private GameObject _phaseTwoHitBoxes;
        [SerializeField] private float _deathExplosionScale;
        #endregion

        private CameraManager _cameraManager;
        private BossController bossController;

        private void Awake() {
            _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
            bossController = GetComponent<BossController>();

            _currentlyHealth = _healthMax;
            GameObject healthBarUI;
            healthBarUI = GameObject.FindGameObjectWithTag("HealthBar");
            
            _healthBar = Instantiate(_healthBarPrefab,healthBarUI.transform).GetComponent<BossHealthBar>();
            _healthBar.SetValueMax(_healthMax);
        }

        public void Damage(int damage){
            if(!bossController._isShieldActive)
                _currentlyHealth -= damage;
            if(_currentlyHealth < _percentStartStage2*_healthMax/100 && !bossController._isSecondStage)
                StartCoroutine(SecondStageCoroutine());
            else if (_currentlyHealth <= 0)
                StartCoroutine(DeathEffectsCoroutine());
            UpdateHealthBar();
        }

        public void UpdateHealthBar(){
            _healthBar.UpdateUI(_currentlyHealth);
        }
        
        private IEnumerator SecondStageCoroutine(){
            bossController. _isShieldActive = true;
            bossController._isSecondStage = true;
            bossController.bossStateMachine.ChangeState(bossController.idleState);

            _cameraManager.CameraShake(_secondPhaseExplosionDuration, 0.1f,false);
            yield return MiniExplosionsCoroutine(_phaseOneHitBoxes, _secondPhaseExplosionDuration);

            for (int index = 0; index < _phaseExplosionPoints.childCount; index++){
                Vector3 explosionPosition = _phaseExplosionPoints.GetChild(index).position;
                Instantiate(_explosion, explosionPosition, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(0.3f);
            SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sprite = _secondPhaseSprite;

            bossController.bossStateMachine.ChangeState(bossController.shieldState);
            StopCoroutine(bossController._changeStateToMissileStateRoutine);
        }

        private IEnumerator DeathEffectsCoroutine(){
            _cameraManager.CameraShake(_deathExplosionDuration, 0.1f,false);
            yield return MiniExplosionsCoroutine(_phaseTwoHitBoxes, _deathExplosionDuration);

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
         
        private void MiniExplosions(GameObject hitBoxesParent){
            Vector3 explodeScale = new Vector3(_miniExplosionScale,_miniExplosionScale,1);
            int hitBoxCount = hitBoxesParent.transform.childCount;
            for(int i = 0; i< hitBoxCount;i++){
                Collider2D hitBox = hitBoxesParent.transform.GetChild(i).GetComponent<Collider2D>();
                Bounds hitBoxArea = hitBox.bounds;
                Vector3 randomPoint = new Vector3(
                    Random.Range(hitBoxArea.min.x,hitBoxArea.max.x),
                    Random.Range(hitBoxArea.min.y,hitBoxArea.max.y), 0);
                GameObject newExplosion = Instantiate(_explosion,randomPoint,Quaternion.identity);
                newExplosion.transform.localScale = explodeScale;
            }
        }
    }
}