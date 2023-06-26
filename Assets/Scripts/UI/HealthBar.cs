using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Texture[] _spriteHealthBar;



    private void Start()
    {
        gameObject.GetComponent<RawImage>().texture = _spriteHealthBar[3];
    }

    public void UpdateHealthBar(int curentlyHealth)
    {
        if(curentlyHealth>0)
            gameObject.GetComponent<RawImage>().texture = _spriteHealthBar[curentlyHealth];
    }
}
