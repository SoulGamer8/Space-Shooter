using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent UnityAction;

    public event UnityAction PlayerDieEvent;
    public event UnityAction<AudioClip> PlayerDieSound;


    [SerializeField] private int _health = 3;


    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;
    [SerializeField] private float _timeWhenActiveShild;

    [Header("VFX")]
    [SerializeField] private GameObject[] _fireOnEngine;

    [Header("Sound")]
    [SerializeField] private AudioClip _explosionSound;


    private int _curentlyShieldHealth;
    private bool _isShieldActivate;
    private GameObject _sheild;


    private GameObject _healthBar;
    private GameManager _gameManager;

    private int _curentlyHealth;


    private void Start()
    {
        _curentlyHealth = _health;
     
    }
    public void Spawn(GameManager gameManager)
    {
        _gameManager = gameManager;
        UnityAction.AddListener(_gameManager.PlayerDead);
    }

    public void AddHealthBar(GameObject healthBar)
    {
        _healthBar = healthBar;
    }

    public void Dead()
    {
        UnityAction?.Invoke();
        PlayerDieEvent?.Invoke();
        PlayerDieSound?.Invoke(_explosionSound);
        Destroy(gameObject,1f); 
    }

    public void TakeDamage(int damage)
    {
        if (_isShieldActivate)
        {
            _curentlyShieldHealth -= damage;

            if (_curentlyShieldHealth <= 0)
            {
                Destroy(_sheild);
                _isShieldActivate = false;
            }

        }

        else
        {
            _curentlyHealth -= damage;
            UpdateHealthBar();
            SpawnFireOnEngine();
        }
        if (_curentlyHealth <= 0)
            Dead();
    }

    private void UpdateHealthBar()
    {
        Debug.Log(_curentlyHealth);
        _healthBar.GetComponent<HealthBar>().UpdateHealthBar(_curentlyHealth);
    }

    public void TakeHeal()
    {
        if (_curentlyHealth != _health)
        {
            _curentlyHealth++;
            UpdateHealthBar();
        }
    }

    public void ActivateShild()
    {
        _curentlyShieldHealth = _shieldHealth;
        StartCoroutine(Sheild());
        _sheild = Instantiate(_shield, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity,transform);
        _isShieldActivate = true;
    }

    public void RespawnPlayer()
    {
       // _gameManager.
    }
    private void SpawnFireOnEngine()
    {
        if(_curentlyHealth - 1 >= 0 )
            Instantiate(_fireOnEngine[_curentlyHealth - 1], transform);
    }

    private IEnumerator Sheild()
    {
        yield return new WaitForSeconds(_timeWhenActiveShild);
        _isShieldActivate = false;
        Destroy(_sheild);
    }
}
