using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Over Screen")]
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private GameObject _gameOverScreen;

    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private JoinNewPlayer _joinNewPlayer;

    [SerializeField] private Score _score;


    private int _playerAlive=0;
    public void PlayerDead()
    {
        _playerAlive--;
        if (_playerAlive == 0)
        {
            _gameOverScreen.SetActive(true);
            gameOverScreen.PlayerDead();

            spawnManager.PlayerDeath();

            _score.OpenRecordMenu();
        }
    }

    public void AddPlayer(GameObject player)
    {
        _playerAlive++;
        player.GetComponent<PlayerHealth>().Spawn(gameObject.GetComponent<GameManager>());
    }



    public void RespawnPlayer() 
    {
        if (_playerAlive < 2)
        {
            _joinNewPlayer.RespawnPlayer();
            _playerAlive++;
        }
    }


    public void RestartGame()
    {
        Debug.Log(_playerAlive);
        Debug.Log(_score.MenuOpen());
        if (_playerAlive <= 0 && _score.MenuOpen())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        _pauseMenu.GetComponent<Animator>().SetTrigger("Activate Pause menu");
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
