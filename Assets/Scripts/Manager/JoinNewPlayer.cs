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

    [SerializeField] private int _amountPlayers=-1;

    private void OnPlayerJoined(PlayerInput playerInput)
   {
        _amountPlayers++;
        GameManager.AddPlayer(playerInput.gameObject);
        CreateHealthBar(playerInput.gameObject);
        Debug.Log(playerInput);

   }


    private void CreateHealthBar(GameObject player)
    {
        GameObject healthBar = Instantiate(_healthBar, _positionHealthBar[_amountPlayers], Quaternion.identity, _canvas.transform);
        player.GetComponent<PlayerHealth>().AddHealthBar(healthBar);
    }
}
