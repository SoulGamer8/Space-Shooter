using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

   
    [SerializeField] private float _speed;


    private Rigidbody2D _rb;


    private void Awake()
    {

        _rb=GetComponent<Rigidbody2D>();
    }

    private void CalculatedMovment(Vector2 moveVecntor)
    {
        if (transform.position.x > 10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        


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
