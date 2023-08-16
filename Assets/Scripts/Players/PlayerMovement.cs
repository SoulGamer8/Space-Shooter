using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

   
    [SerializeField] private float _speed;

    [Header("Power Up")]
    [SerializeField] private float _timeWhenActiveSpeedPowerUp=5f;
    [SerializeField] private float _speedPowerUp;
    private bool _isSpeedPowerUpActive = false; 

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

   

    public void OnMovevmentPerformed(InputAction.CallbackContext value)
    {
        CalculatedMovment(value.ReadValue<Vector2>());

       
        var vector2 = value.ReadValue<Vector2>();
        if(vector2.x == 0)
            _animator.SetTrigger("Stop");
        _animator.SetFloat("Vector2", vector2.x);
    }

    private void CalculatedMovment(Vector2 moveVecntor)
    {
        float speed = _isSpeedPowerUpActive ? _speedPowerUp : _speed;

        _rb.velocity = moveVecntor * speed;
    }

    public void TakeSpeedPowerUp()
    {
        StartCoroutine(SpeedPowerUp());
        _isSpeedPowerUpActive = true;
    }


    private IEnumerator SpeedPowerUp()
    {
        yield return new WaitForSeconds(_timeWhenActiveSpeedPowerUp);

        _isSpeedPowerUpActive = false;
    }
}
