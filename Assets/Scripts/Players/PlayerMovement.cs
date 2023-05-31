using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

   
    [SerializeField] private float _speed;


    private Rigidbody _rb;


    private void Awake()
    {

        _rb=GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.x > 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4),0);
    }

    private void CalculatedMovment(Vector2 moveVecntor)
    {
        _rb.velocity = moveVecntor * _speed;
    }

    public void OnMovevmentPerformed(InputAction.CallbackContext value)
    {
        CalculatedMovment(value.ReadValue<Vector2>());
    }

    public void OnMovevmentCanceled(InputAction.CallbackContext value)
    {
        CalculatedMovment(value.ReadValue<Vector2>());
    }
}
