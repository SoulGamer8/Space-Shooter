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

        _playerAlive++;
        player.GetComponent<PlayerHealth>().Spawn(gameObject.GetComponent<GameManager>());
    }


    public void ApplicaionQuit()
    {
        Application.Quit();
    }
}
