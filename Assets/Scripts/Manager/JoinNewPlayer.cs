using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinNewPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;

    [SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _player;

    [SerializeField] private Transform[] _healthBarTransform;


    private List<GameObject> _playersList;
     private int _amountPlayers=-1;


    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log(_playersList.Count);
        if(_playersList.Count != 2)
        {
            _playersList.Add(playerInput.gameObject);
            GameManager.AddPlayer(playerInput.gameObject);
            CreateHealthBar(playerInput.gameObject);
            DontDestroyOnLoad(playerInput.gameObject);
        }

    }

    private void CreateHealthBar(GameObject player)
    {

        GameObject healthBar = Instantiate(_healthBar, _healthBarTransform[_playersList.Count].transform.position, Quaternion.identity, _canvas.transform.transform);

        player.GetComponent<PlayerHealth>().AddHealthBar(healthBar);
    }
}
