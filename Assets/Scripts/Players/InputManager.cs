using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController _input = null;

    [SerializeField] private Shoot _shoot;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private DeadScreen _deadSceen;

    private void Awake()
    {
        _input = new PlayerController();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += _playerMovement.OnMovevmentPerformed;
        _input.Player.Movement.canceled += _playerMovement.OnMovevmentCanceled;

        _input.Player.Shoot.performed += _ => _shoot.FireBullet();

        _input.Player.Restart.performed += _ => _deadSceen.ReloadScene();
    }


    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Movement.performed += _playerMovement.OnMovevmentPerformed;
        _input.Player.Movement.canceled += _playerMovement.OnMovevmentCanceled;


        _input.Player.Shoot.performed -= _ => _shoot.FireBullet();

    }


}
