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

    private int _curentlyHealth;
    private HealthBar _healthBar;
    #endregion

    [Header("Invicible")]
    
    [SerializeField] private float _timeWhenPlayerInvulnerability;
    [SerializeField] private float _invicibleAlpa;
    
    #region  Shield
    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;
    [SerializeField] private float _timeWhenActiveShild;

    private int _curentlyShieldHealth;
    private bool _isShieldActivate;
    private GameObject _sheild;
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

        _curentlyHealth = _health;

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

    public void Damege(int damage){
        if (_isShieldActivate){
            _curentlyShieldHealth -= damage;
            if (_curentlyShieldHealth <= 0)
            {
                Destroy(_sheild);
                _isShieldActivate = false;
            }
        }
        else{
            _curentlyHealth -= damage;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelProgressManager>().PlayerTakeDamage();

            UpdateHealthBar();
            SpawnFireOnEngine();
            _cameraManager.CameraShake(_timeWhenPlayerInvulnerability,0.1f,true);
            playerSoundManager.PlaySound(_soudTakeDamage);
            StartCoroutine(InvulnerabilityCoroutine());
            if (_curentlyHealth <= 0)
                Dead();
            _playerSoundManager.PlaySound(_hitSound);
        }
    }

    private IEnumerator InvulnerabilityCoroutine(){
        _polygonCollider2D.enabled = false;
        float timer = 0;

        
        SpriteRenderer spriteRenderer =  this.gameObject.GetComponent<SpriteRenderer>();
        Color myColor = spriteRenderer.color;
        
        Color myAlphaColor = new Color(myColor.r,myColor.g,myColor.b,_invicibleAlpa);

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
        if (_curentlyHealth - 1 >= 0)
        {
            GameObject fire = Instantiate(_fireOnEngine[_fireOnEngine.Length - _curentlyHealth], transform);
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
        _healthBar.UpdateHealthBar(_curentlyHealth);
    }
    #endregion

    #region PowerUp
    public void TakeHeal(){
        if (_curentlyHealth != _health)
        {
            _curentlyHealth++;
            Destroy(_fireOnEngineInstiate[_fireOnEngineInstiate.Count - 1]);
            _fireOnEngineInstiate.RemoveAt(_fireOnEngineInstiate.Count-1);
            UpdateHealthBar();
            _powerUpWeightController.ChangeSpawnChacneWeightRepair(-10);
        }
    }


    public void ActivateShild(){
        _curentlyShieldHealth = _shieldHealth;
        StartCoroutine(SheildCoroutine());
        _sheild = Instantiate(_shield, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity,transform);
        _isShieldActivate = true;
    }

    
    private IEnumerator SheildCoroutine(){
        yield return new WaitForSeconds(_timeWhenActiveShild);
        _isShieldActivate = false;
        Destroy(_sheild);
    }
    

    public void RespawnPlayer(){
        _playersController.RespawnPlayer();

    }
    #endregion


   
}
