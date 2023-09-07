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
    [SerializeField] float a,b;
    [SerializeField] private int divisons;
    [SerializeField] private Vector3[] positions;
    private float dangle;
    private int curentlyDivision=20;
    #endregion
    public override int GetSpawnChanceWeight() => _spawnChanceWeight;


    public override void Start(){
        base.Start();
        positions = new Vector3[divisons +1];
        dangle = 2*Mathf.PI /divisons;
        Shoot();
        CreateOval();
    }

    private void Update() {
        DoMove();
    }

    public void DoMove(){
        transform.position = Vector3.MoveTowards(transform.position,positions[curentlyDivision],0.1f);
        if(Vector3.Distance(transform.position,positions[curentlyDivision]) <0.1f){
                if(curentlyDivision >= divisons)
                    curentlyDivision=0;
                else
                    curentlyDivision++;
            }
    }

    private void CreateOval(){
        float angle= 0;
        Vector3 position = new Vector3(0,5,0);
        for(int i = 0; i < divisons+1;i++,angle +=dangle){
            position.x = a * Mathf.Cos(angle);
            position.y = b * Mathf.Sin(angle)+5;
            positions[i] = position;
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
