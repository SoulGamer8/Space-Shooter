using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] private float _timeHowLongLive;
    [SerializeField] private GameObject _menuCompleteLevel;


    private void Start()
    {
        StartCoroutine(Timer());
    }


    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_timeHowLongLive);
        LevelManager._isWin = true;
        LevelManager._scene = SceneManager.GetActiveScene();
        _menuCompleteLevel.SetActive(true);
        Debug.Log("Level complete");
    } 
}
