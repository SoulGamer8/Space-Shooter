using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Texture[] _spriteHealthBar;


    private int curentlyHealth = 3 ;


    private void Start()
    {
        gameObject.GetComponent<RawImage>().texture = _spriteHealthBar[curentlyHealth];
    }

    public void UpdateHealthBar(int damage)
    {
        curentlyHealth -= damage;
        gameObject.GetComponent<RawImage>().texture = _spriteHealthBar[curentlyHealth];
    }
}
