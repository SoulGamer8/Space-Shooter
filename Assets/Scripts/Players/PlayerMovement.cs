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

    private enum MovingState{Start,Moving,Exit}
    [SerializeField] private MovingState _myState;

    private Rigidbody2D _rb;
    private Animator _animator;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb =GetComponent<Rigidbody2D>();
    }

    private void Start(){
        transform.position = new Vector3(Random.Range(-7,7),-10,0);
    }

    private void Update(){
        Act();
    }

    private void Act(){
        switch((int)_myState){
            case 0:
                SpawnShip();
                break;
            case 1:
                AreaLimiter();
                break;
            case 2:
                OnExit();
                break;
        }
    }

    private void  SpawnShip(){
        transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x,-2),0.2f);
        if(Vector3.Distance(transform.position,new Vector3(transform.position.x,-2))<0.1f)
            _myState= MovingState.Moving;
    }

    private void AreaLimiter(){
         if (transform.position.x > 10)
            transform.position = new Vector3(10, transform.position.y, transform.position.z);

        if (transform.position.x < -10)
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4),0);
    }

    private void OnExit(){
        transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x,10),0.1f);
        if(Vector3.Distance(transform.position,new Vector3(transform.position.x,10))<0.1f)
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelProgressManager>().LevelComplete();
    }

    
    public void LevelComplete(){
       StartCoroutine(WaitBossDiedCoroutine());
    }

    public void OnMovevmentPerformed(InputAction.CallbackContext value){
        if(_myState == MovingState.Start)
            return;
        CalculatedMovment(value.ReadValue<Vector2>());

       
        var vector2 = value.ReadValue<Vector2>();
        if(vector2.x == 0)
            _animator.SetTrigger("Stop");
        _animator.SetFloat("Vector2", vector2.x);
    }

    private void CalculatedMovment(Vector2 moveVector){
        float speed = _isSpeedPowerUpActive ? _speedPowerUp : _speed;

        _rb.velocity = moveVector * speed;
    }

    #region PowerUp
    public void TakeSpeedPowerUp(){
        StartCoroutine(SpeedPowerUpCoroutine());
        _isSpeedPowerUpActive = true;
    }


    private IEnumerator SpeedPowerUpCoroutine(){
        yield return new WaitForSeconds(_timeWhenActiveSpeedPowerUp);

        _isSpeedPowerUpActive = false;
    }
    #endregion

    
    private IEnumerator WaitBossDiedCoroutine(){
        yield return new WaitForSeconds(2);
         _myState = MovingState.Exit;

    }
}
