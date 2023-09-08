using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    public UnityEvent UnityAction;

    public event UnityAction PlayerDieEvent;
    public event UnityAction<AudioClip> PlayerDieSound;

    #region Health 
    [Header("Health")]
    [SerializeField]private GameObject[] _healthBarArray;
    [SerializeField] private int _health = 3;
    [SerializeField] private AudioClip _soudTakeDamage;

    private int _currentlyHealth;
    private HealthBar _healthBar;
    #endregion

    [Header("Invincible")]
    
    [SerializeField] private float _timeWhenPlayerInvulnerability;
    [SerializeField] private float _invincibleAlpha;
    
    #region  Shield
    [Header("Shield")]
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private int _shieldHealth;
    [SerializeField] private float _timeWhenActiveShield;

    private int _currentlyShieldHealth;
    private bool _isShieldActivate;
    private GameObject _shield;
    #endregion

    [Header("VFX")]
    [SerializeField] private GameObject[] _fireOnEngine;
    [SerializeField] private List<GameObject> _fireOnEngineInstiate;

    #region Sound
    [Header("Sound")]
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _explosionSound;
    private PlayerSoundManager _playerSoundManager;
    #endregion

    #region Component
    private PlayerSoundManager playerSoundManager;
    private CameraManager _cameraManager;
    private PolygonCollider2D _polygonCollider2D;
    private Animator _animator;
    private PlayersController _playersController;
    private PowerUpWeightController _powerUpWeightController;
    #endregion



    private void Awake(){
        FindHealthBar();

        _currentlyHealth = _health;

        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _playerSoundManager = GetComponent<PlayerSoundManager>();
        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        playerSoundManager = GetComponent<PlayerSoundManager>();

        _powerUpWeightController = PowerUpWeightController.instance;
        if(_powerUpWeightController == null)
              throw new System.NullReferenceException("Power Up Weight Controller not found");
    }

    public void Spawn(PlayersController playersController){
        _playersController = playersController;
        UnityAction.AddListener(playersController.PlayerDead);
    }

    public void Damage(int damage){
        if (_isShieldActivate){
            _currentlyShieldHealth -= damage;
            if (_currentlyShieldHealth <= 0)
            {
                Destroy(_shield);
                _isShieldActivate = false;
            }
        }
        else{
            _currentlyHealth -= damage;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelProgressManager>().PlayerTakeDamage();

            UpdateHealthBar();
            SpawnFireOnEngine();
            _cameraManager.CameraShake(_timeWhenPlayerInvulnerability,0.1f,true);
            playerSoundManager.PlaySound(_soudTakeDamage);
            StartCoroutine(InvulnerabilityCoroutine());
            if (_currentlyHealth <= 0)
                Dead();
            _playerSoundManager.PlaySound(_hitSound);
        }
    }

    private IEnumerator InvulnerabilityCoroutine(){
        _polygonCollider2D.enabled = false;
        float timer = 0;

        
        SpriteRenderer spriteRenderer =  this.gameObject.GetComponent<SpriteRenderer>();
        Color myColor = spriteRenderer.color;
        
        Color myAlphaColor = new Color(myColor.r,myColor.g,myColor.b,_invincibleAlpha);

        while(timer <_timeWhenPlayerInvulnerability){
            timer += Time.deltaTime;
            float lerpProgress = Mathf.Pow(Mathf.Sin(timer *(Mathf.PI/2)/_timeWhenPlayerInvulnerability),2);
            spriteRenderer.color = Color.Lerp(myColor,myAlphaColor,lerpProgress);
            yield return null;
        }

        spriteRenderer.color = myColor;
        _polygonCollider2D.enabled = true;
    }

    private void SpawnFireOnEngine(){
        if (_currentlyHealth - 1 >= 0)
        {
            GameObject fire = Instantiate(_fireOnEngine[_fireOnEngine.Length - _currentlyHealth], transform);
            _fireOnEngineInstiate.Add(fire);
        }
    }

    public void Dead(){
        UnityAction?.Invoke();
        PlayerDieEvent?.Invoke();
        PlayerDieSound?.Invoke(_explosionSound);
        _animator.SetTrigger("PlayerDead");
        Destroy(gameObject, 1f);
    }



    #region HealthBar
    private void FindHealthBar(){
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Player");
        GameObject canvas = GameObject.FindGameObjectWithTag("HealthBar");
        
        GameObject healthBar;
        if(canvas == null)
            throw new System.NullReferenceException("HealthBar not found");
        
        healthBar = Instantiate(_healthBarArray[gameObject.Length-1],canvas.transform);

        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.MaxHealth(_health);
    }

    private void UpdateHealthBar(){
        _healthBar.UpdateHealthBar(_currentlyHealth);
    }
    #endregion

    #region PowerUp
    public void TakeHeal(){
        if (_currentlyHealth != _health)
        {
            _currentlyHealth++;
            Destroy(_fireOnEngineInstiate[_fireOnEngineInstiate.Count - 1]);
            _fireOnEngineInstiate.RemoveAt(_fireOnEngineInstiate.Count-1);
            UpdateHealthBar();
            _powerUpWeightController.ChangeSpawnChanceWeightRepair(-10);
        }
    }


    public void ActivateShied(){
        _currentlyShieldHealth = _shieldHealth;
        StartCoroutine(ShieldCoroutine());
        _shield = Instantiate(_shieldPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity,transform);
        _isShieldActivate = true;
    }

    
    private IEnumerator ShieldCoroutine(){
        yield return new WaitForSeconds(_timeWhenActiveShield);
        _isShieldActivate = false;
        Destroy(_shield);
    }
    

    public void RespawnPlayer(){
        _playersController.RespawnPlayer();

    }
    #endregion


   
}
