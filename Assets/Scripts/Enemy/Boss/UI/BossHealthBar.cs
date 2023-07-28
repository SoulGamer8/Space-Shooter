using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;


    public void UpdateUI(int health)
    {
        _healthSlider.value = health;
    }
}
