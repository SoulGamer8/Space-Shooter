using System.Collections;
using UnityEngine;

public class ShipOvalEnemy : Enemy, ICanShoot, IMoveable
{


    #region Laser
    [SerializeField] private GameObject _laser;
    [SerializeField] private float _fireRate;
    #endregion

    #region  Oval Size
    [Header("Oval size")]
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private int _divisions;
    [SerializeField] private Vector3[] _positions;
    private float _dangle;
    private int _currentlyDivision=20;
    #endregion

    public override void Start(){
        base.Start();
        _positions = new Vector3[_divisions +1];
        _dangle = 2*Mathf.PI /_divisions;
        Shoot();
        CreateOval();
    }

    private void Update() {
        DoMove();
    }

    public void DoMove(){
        transform.position = Vector3.MoveTowards(transform.position,_positions[_currentlyDivision],_speed*Time.deltaTime);
        if(Vector3.Distance(transform.position,_positions[_currentlyDivision]) <0.1f){
                if(_currentlyDivision >= _divisions)
                    _currentlyDivision=0;
                else
                    _currentlyDivision++;
            }
    }

    private void CreateOval(){
        float angle= 0;
        Vector3 position = new Vector3(0,5,0);
        for(int i = 0; i < _divisions+1;i++,angle +=_dangle){
            position.x = _width * Mathf.Cos(angle);
            position.y = _height * Mathf.Sin(angle)+5;
            _positions[i] = position;
        }
        
    }

    public void Shoot(){
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine(){
        while(true){
            yield return new WaitForSeconds(_fireRate);
            Instantiate(_laser,transform.position,Quaternion.identity);
        }   
     }
 
}
