using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

   
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWhenActiveSpeedPowerUp=5f;


    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb =GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.x > 10)
            transform.position = new Vector3(10, transform.position.y, transform.position.z);

        if (transform.position.x < -10)
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4),0);
    }

    private void CalculatedMovment(Vector2 moveVecntor)
    {
        _rb.velocity = moveVecntor * _speed;
    }

    public void OnMovevmentPerformed(InputAction.CallbackContext value)
    {
        CalculatedMovment(value.ReadValue<Vector2>());

       
        var vector2 = value.ReadValue<Vector2>();
        if(vector2.x == 0)
            _animator.SetTrigger("Stop");
        _animator.SetFloat("Vector2", vector2.x);
    }

    public void TakeSpeedPowerUp()
    {
        StartCoroutine(SpeedPowerUp());
        _speed *= 2;
    }


    private IEnumerator SpeedPowerUp()
    {
        yield return new WaitForSeconds(_timeWhenActiveSpeedPowerUp);

        _speed /= 2;
    }
}
