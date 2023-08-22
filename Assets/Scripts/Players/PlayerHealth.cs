using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    public UnityEvent UnityAction;

    public event UnityAction PlayerDieEvent;
    public event UnityAction<AudioClip> PlayerDieSound;

    [Header("Health")]
    [SerializeField]private GameObject[] _healthBarArray;
    [SerializeField] private int _health = 3;
    [SerializeField] private float _timeWhenPlayerInvulnerability;

    private int _curentlyHealth;
    private HealthBar _healthBar;

    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;
    [SerializeField] private float _timeWhenActiveShild;

    private int _curentlyShieldHealth;
    private bool _isShieldActivate;
    private GameObject _sheild;

    [Header("VFX")]
    [SerializeField] private GameObject[] _fireOnEngine;

    [SerializeField] private List<GameObject> _fireOnEngineInstiate;

    [Header("Sound")]
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _explosionSound;
    private PlayerSoundManager _playerSoundManager;

    private PolygonCollider2D _polygonCollider2D;
    private Animator _animator;
    private PlayersController _playersController;
    private PowerUpWeightController _powerUpWeightController;


    private void Awake(){
        _curentlyHealth = _health;
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _playerSoundManager = GetComponent<PlayerSoundManager>();
        FindHealthBar();
        _powerUpWeightController = PowerUpWeightController.instance;

        
        Debug.Log(_powerUpWeightController);
    }

    public void Spawn(PlayersController playersController){
        _playersController = playersController;
        UnityAction.AddListener(playersController.PlayerDead);
    }


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
            UpdateHealthBar();
            SpawnFireOnEngine();
            StartCoroutine(Invulnerability());
            if (_curentlyHealth <= 0)
                Dead();
            // _powerUpWeightController.ChangeSpawnChacneWeightRepair(10);
            _playerSoundManager.PlaySound(_hitSound);
        }
        
    }

    private void UpdateHealthBar(){
        _healthBar.UpdateHealthBar(_curentlyHealth);
    }

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
        StartCoroutine(Sheild());
        _sheild = Instantiate(_shield, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity,transform);
        _isShieldActivate = true;
    }

    public void RespawnPlayer(){
        _playersController.RespawnPlayer();

    }
    private void SpawnFireOnEngine(){
        if (_curentlyHealth - 1 >= 0)
        {
            Debug.Log(_fireOnEngine.Length - _curentlyHealth);
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


    private IEnumerator Sheild(){
        yield return new WaitForSeconds(_timeWhenActiveShild);
        _isShieldActivate = false;
        Destroy(_sheild);
    }
    
    private IEnumerator Invulnerability(){
        Coroutine blink = StartCoroutine(Blink());
        _polygonCollider2D.enabled = false;
        yield return new WaitForSeconds(_timeWhenPlayerInvulnerability);
        StopCoroutine(blink);
        _polygonCollider2D.enabled = true;
        
    }

    private IEnumerator Blink(){
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        var tempColor = spriteRenderer.color;
        while(true){  
            tempColor.a = 1f;
            spriteRenderer.color = tempColor;
            yield return new WaitForSeconds(0.1f);
            tempColor.a = 0f;
            spriteRenderer.color = tempColor;
            yield return new WaitForSeconds(0.1f);

        }
    }
}
