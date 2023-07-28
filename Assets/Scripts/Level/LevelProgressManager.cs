using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] private float _timeHowLongLive;
    [SerializeField] private GameObject _menuCompleteLevel;

    [SerializeField] private bool _isHasBoss=false;
    [ConditionalHide("_isHasBoss", true)]
    [SerializeField] private GameObject _boosPrefab;
    private WalletManager _walletManager;
    private void Start()
    {
        _walletManager = WalletManager.instance;
        StartCoroutine(Timer());
    }


    private void SpawnBoss()
    {
        Instantiate(_boosPrefab);
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_timeHowLongLive);
        if (!_isHasBoss ) 
        {
            LevelManager._isWin = true;
            LevelManager._scene = SceneManager.GetActiveScene().name;
            _menuCompleteLevel.SetActive(true);
            _walletManager.SaveMoney();
            Debug.Log("Level complete");

        }
        else
        {
            SpawnBoss();
        }

    } 
}
