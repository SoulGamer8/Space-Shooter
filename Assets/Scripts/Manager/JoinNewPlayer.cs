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

    [SerializeField] private GameController _gameController;

     private PlayerInputManager _inputManager;

     private int _amountPlayers=-1;


    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        //OnPlayerJoined(_inputManager.playerPrefab.GetComponent<PlayerInput>());
    }

    private void OnPlayerJoined(PlayerInput playerInput)
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
