using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemyLaser : Ammo
{
    private int _bulletSpeed;
    private int _bulletDamage;
    private Vector3 _playerPosition;
    
    public void SetTarget(Vector3 target){
        Vector3 temp = transform.position - target;
       _playerPosition = target - temp*2;
    }

    public void SetSpeed(int speed){
        _bulletSpeed = speed;
    }

    public void SetDamage(int damage){
        _bulletDamage = damage;
    }

    private void Update() {
        DoMove();
    }

    public override void DoMove()
    {

        transform.position = Vector3.MoveTowards(transform.position, _playerPosition, 1*Time.deltaTime*_bulletSpeed);

        if (transform.position.y > 10 || transform.position.y < -10 ||(Vector3.Distance(transform.position, _playerPosition) < 0.1f))
            Dead();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        
        if(damageable != null  && collider.tag != "Enemy")
        {
            damageable.Damege(_bulletDamage);
            Dead();
        }
    }

    protected override void Dead()
    {
        Destroy(gameObject);
    }

}
