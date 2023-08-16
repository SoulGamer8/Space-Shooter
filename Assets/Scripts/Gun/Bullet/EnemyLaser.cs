using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : Ammo
{ 
    private int _bulletSpeed=1;
    private int _bulletDamage;
    private bool _trippleShootIsActive = false;
    private float _trippleShootSpeed;

    public void SetDamageAndSpeed(int damage,int speed)
    {
        if(damage >0)
            _bulletDamage = damage;
        if(speed >0)
            _bulletSpeed = speed;
    }
    
    private void Start()
    {
        _trippleShootSpeed = _bulletSpeed;
        _bulletSpeed *= -1;
    }

    void Update()
    {
      DoMove();
    }

     

    protected override void DoMove(){
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (_trippleShootIsActive)
        {
            transform.position += new Vector3(_trippleShootSpeed, _bulletSpeed, 0) * Time.deltaTime;
        }

        if (transform.position.y > 10 || transform.position.y < -10)
            Dead();
    }

    public void Change(float isRight)
    {
        _trippleShootIsActive = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
         IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null && collider.tag =="Player")
        {
            damageable.Damege(_bulletDamage);
            Dead();
        }
    }

    protected override void Dead()
    {
        if (transform.parent != null)
        {
           if (gameObject.transform.parent.transform.childCount == 1 )
                Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

   
}
