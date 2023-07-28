using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int _healthMax;
    [SerializeField] private BossHealthBar _healthBar;
    private int _curentlyHealth;

    private void Awake()
    {
        _curentlyHealth = _healthMax;
    }

    public void TakeDamage(int damage)
    {
       _curentlyHealth -= damage;
        if (_curentlyHealth < 0)
            Death();
        UpdateHealthBar();
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
