using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public static WalletManager instance;


    private int _money;

    private TextMeshProUGUI _moneyText;

    private void Awake()
    {

        _money = PlayerPrefs.GetInt("Money");
        _moneyText = GetComponent<TextMeshProUGUI>();
        ChangeUI();
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    public void SetMoney(int money)
    {
        _money += money;
       
        ChangeUI();
    }

    public int GetMoney()
    {
        return _money;
    }

    private void ChangeUI()
    {
        _moneyText.text ="Money:" +  _money.ToString();
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", _money);
    }
}
