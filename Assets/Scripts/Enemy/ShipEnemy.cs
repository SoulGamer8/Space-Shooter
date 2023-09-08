using System.Collections;
using UnityEngine;


public class ShipEnemy : Enemy, IMoveable, ICanShoot
{
    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate;


    private void Awake() {
        Shoot();
    }

    private void Update(){
      DoMove();
    }
    
    public void DoMove(){
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    public  void Shoot(){
       StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine(){
        while (true)
        {
            Instantiate(_bullet, new Vector3(transform.position.x,transform.position.y - 2.1f, 0),Quaternion.identity,transform);
            yield return new WaitForSeconds(_fireRate);
        }
       
    }
}
