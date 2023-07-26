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

    private PlayerInputManager _playerInputManager;
     private int _amountPlayers=-1;


    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void RespawnPlayer()
    {
        _playerInputManager.playerPrefab = _player;
        _playerInputManager.JoinPlayer();
        OnPlayerJoined(_player);
    }

    private  void OnPlayerJoined(PlayerInput playerInput)
    {
        _amountPlayers++;
        GameManager.AddPlayer(playerInput.gameObject);
        CreateHealthBar(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);

    }

    private void OnPlayerJoined(GameObject playerInput)
    {
        _amountPlayers++;
        GameManager.AddPlayer(playerInput.gameObject);
        CreateHealthBar(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);

    }

    private void CreateHealthBar(GameObject player)
    {

        GameObject healthBar = Instantiate(_healthBar, _healthBarTransform[_amountPlayers].transform.position, Quaternion.identity, _canvas.transform.transform);

        player.GetComponent<PlayerHealth>().AddHealthBar(healthBar);
    }


}
