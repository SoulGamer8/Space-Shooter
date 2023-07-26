using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController _input;

    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _input = new PlayerController();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.UI.Exit.performed += _ => _gameManager.PauseGame();
        _input.UI.Restart.started += _ => _gameManager.RestartGame();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
