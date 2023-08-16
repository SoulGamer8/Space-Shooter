using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayersController : MonoBehaviour
{

    [SerializeField] private JoinNewPlayer _joinNewPlayer;
    [SerializeField] private GameManager _gameManager;
    
    private int _playerAlive = 0;


    public void PlayerDead()
    {
        _playerAlive--;
        if (_playerAlive == 0)
            _gameManager.AllPlayerDead();

    }

    public void AddPlayer(GameObject player)
    {
        _playerAlive++;
        player.GetComponent<PlayerHealth>().Spawn(gameObject.GetComponent<PlayersController>());
    }



    public void RespawnPlayer()
    {
        if (_playerAlive < 2)
        {
            _joinNewPlayer.RespawnPlayer();
            _playerAlive++;
        }
    }

}
