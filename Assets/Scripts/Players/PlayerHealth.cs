using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent UnityAction;

    public event UnityAction PlayerDieEvent;
    public event UnityAction<AudioClip> PlayerDieSound;

    [Header("Health")]
    [SerializeField] private int _health = 3;

    private int _curentlyHealth;
    private GameObject _healthBar;

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
    [SerializeField] private AudioClip _explosionSound;

    [SerializeField] private float _timeWhenPlayerInvulnerability;


    private bool _isInvulnerabilityAactivated = false;

    private PolygonCollider2D _polygonCollider2D;

    private GameManager _gameManager;

    private Animator _animator;

    private void Awake()
    {
        _curentlyHealth = _health;
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
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
        _animator.SetTrigger("PlayerDead");
        this.gameObject.SetActive(false);
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

        if(!_isInvulnerabilityAactivated)
        {
            _curentlyHealth -= damage;
            UpdateHealthBar();
            SpawnFireOnEngine();
            StartCoroutine(Invulnerability());
            
        }
        if (_curentlyHealth <= 0)
            Dead();
    }

    private void UpdateHealthBar()
    {
        _healthBar.GetComponent<HealthBar>().UpdateHealthBar(_curentlyHealth);
    }

    public void TakeHeal()
    {
        if (_curentlyHealth != _health)
        {
            _curentlyHealth++;
            Destroy(_fireOnEngineInstiate[_fireOnEngineInstiate.Count - 1]);
            _fireOnEngineInstiate.RemoveAt(_fireOnEngineInstiate.Count-1);
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
        _gameManager.RespawnPlayer();

    }
    private void SpawnFireOnEngine()
    {
        if (_curentlyHealth - 1 >= 0)
        {
            GameObject fire = Instantiate(_fireOnEngine[_curentlyHealth - 1], transform);
            _fireOnEngineInstiate.Add(fire);
        }
    }

    private IEnumerator Sheild()
    {
        yield return new WaitForSeconds(_timeWhenActiveShild);
        _isShieldActivate = false;
        Destroy(_sheild);
    }
    
    private IEnumerator Invulnerability()
    {
        _polygonCollider2D.enabled = false;
        yield return new WaitForSeconds(_timeWhenPlayerInvulnerability);
        _polygonCollider2D.enabled = true;
    }
}
