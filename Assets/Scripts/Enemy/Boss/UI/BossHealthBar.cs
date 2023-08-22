using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider _healthSlider;

    private void Awake() {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetValueMax(int maxHealth){
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void UpdateUI(int health){
        _healthSlider.value = health;
    }
}
