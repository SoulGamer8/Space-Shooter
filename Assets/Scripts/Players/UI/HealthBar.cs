using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _healthSlider;




    private void Awake()
    {
        _healthSlider = gameObject.GetComponentInChildren<Slider>();
    }

    public void MaxHealth(int maxHealth){
        _healthSlider.maxValue = maxHealth;
    }


    public void UpdateHealthBar(int curentlyHealth)
    {
        if(curentlyHealth>0)
            _healthSlider.value = curentlyHealth;
    }
}
