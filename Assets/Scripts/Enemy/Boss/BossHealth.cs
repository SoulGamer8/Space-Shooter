using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int _healthMax;
    [SerializeField] private BossHealthBar _healthBar;
    private int _curentlyHealth;
    private BossStateController _bossStateController;
    private void Awake()
    {
        _curentlyHealth = _healthMax;
    }

    public void TakeDamage(int damage)
    {
        _curentlyHealth -= damage;
        if(_curentlyHealth > 250)
            Debug.Log("Stage 1");
        if(_curentlyHealth <= 250 && _curentlyHealth >=100){            
            Debug.Log("Stage 2");
            }

        if (_curentlyHealth < 0)
            Death();
        UpdateHealthBar();
    }

    private void FirstPhase(){

    }

    private void UpdateHealthBar()
    {
        _healthBar.UpdateUI(_curentlyHealth);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
