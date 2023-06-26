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

    [SerializeField] private Vector3[] _positionHealthBar;
    private void Start()
    {
        Instantiate(_player);
    }

    private void OnPlayerJoined(PlayerInput playerInput)
   {
        GameManager.AddPlayer(playerInput.gameObject);
        CreateHealthBar(playerInput.gameObject);
   }


    private void CreateHealthBar(GameObject player)
    {
        GameObject healthBar = Instantiate(_healthBar, _positionHealthBar[0], Quaternion.identity, _canvas.transform);
        player.GetComponent<PlayerHealth>().AddHealthBar(healthBar);
    }
}
