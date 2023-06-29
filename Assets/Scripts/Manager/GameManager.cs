using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private GameObject[] _playerArray = new GameObject[2];
    int _playerAlive=0;

   
    public void PlayerDead()
    {
        _playerAlive--;
        if (_playerAlive == 0)
        {
            gameOverScreen.PlayerDead();
            spawnManager.PlayerDeath();
        }
      
    }

    public void AddPlayer(GameObject player)
    {
        _playerArray[_playerAlive] = player; 
        _playerAlive++;
        player.GetComponent<PlayerHealth>().Spawn(gameObject.GetComponent<GameManager>());
    }

    public void RespawnPlayer()
    {

    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
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
