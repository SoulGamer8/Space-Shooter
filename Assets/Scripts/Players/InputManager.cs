using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController _input = null;

    private Shoot _shoot;
    private PlayerMovement _playerMovement;


    private void Awake()
    {
        _input = new PlayerController();

        _shoot = GetComponent<Shoot>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += _playerMovement.OnMovevmentPerformed;
        _input.Player.Movement.canceled += _playerMovement.OnMovevmentCanceled;

        _input.Player.Shoot.performed += _ => _shoot.FireBullet();
    }


    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Movement.performed += _playerMovement.OnMovevmentPerformed;
        _input.Player.Movement.canceled += _playerMovement.OnMovevmentCanceled;


        _input.Player.Shoot.performed -= _ => _shoot.FireBullet();

    }


}
