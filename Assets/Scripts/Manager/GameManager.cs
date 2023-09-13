using System.ComponentModel;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private bool _isInfinityLevel=false;


    [SerializeField] private Score _score;

    private bool _isAllPlayerDied=false;


    public void AllPlayerDead(){
        _gameOverScreen.SetActive(true);

        spawnManager.PlayerDeath();
        
        if(_isInfinityLevel)
            _score.OpenRecordMenu();

        _isAllPlayerDied = true;
    }

    public void RestartGame(){
        if(_score == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (_isAllPlayerDied  && (_score.IsNewRecordMenuOpen()|| !_isInfinityLevel))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }

    #region Pause Menu
    public void PauseGame(){
        if(_pauseMenu.activeSelf){
            ResumeGame();
            return;
        }
        _pauseMenu.SetActive(true);
        _pauseMenu.GetComponent<Animator>().SetTrigger("Activate Pause menu");
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        StartCoroutine(ResumeGameCoroutine());
    }

    private IEnumerator ResumeGameCoroutine(){
        _pauseMenu.GetComponent<Animator>().SetTrigger("Disable Pause Menu");
        yield return new WaitForSecondsRealtime(_pauseMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    #endregion
}
