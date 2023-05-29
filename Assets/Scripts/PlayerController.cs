using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

   
    [SerializeField] private float _speed;

    private PlayerControllerInput _input = null;
    private Vector2 _moveVecntor;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _input = new PlayerControllerInput();
        _rb=GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += OnMovevmentPerformed;
        _input.Player.Movement.canceled += OnMovevmentCanceled;
    }


    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Movement.performed -= OnMovevmentPerformed;
        _input.Player.Movement.canceled -= OnMovevmentCanceled;
    }

    private void FixedUpdate()
    {
        Debug.Log(_moveVecntor);
        _rb.velocity = _moveVecntor * _speed;
    }

    private void OnMovevmentPerformed(InputAction.CallbackContext value)
    {
        _moveVecntor= value.ReadValue<Vector2>();
    }

    private void OnMovevmentCanceled(InputAction.CallbackContext value)
    {
        _moveVecntor = Vector2.zero;
    }
}
