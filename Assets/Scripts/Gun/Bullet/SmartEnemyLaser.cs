using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemyLaser : Ammo
{
    private Vector3 _target;

    private  void Awake() {
        SetTarget();
    }


    private void SetTarget(){
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        int randomPlayer = UnityEngine.Random.Range(0, player.Length);
        Vector3 playerPosition  = player[randomPlayer].transform.position;
        _target = playerPosition - (transform.position - playerPosition)*2;


        float angelToFire = Vector3.SignedAngle(Vector3.up ,playerPosition-transform.position, Vector3.forward);
        transform.rotation = Quaternion.Euler(0,0,angelToFire);
    }

    private void Update() {
        DoMove();
    }

    public override void DoMove()
    {

        transform.position = Vector3.MoveTowards(transform.position, _target, 1*Time.deltaTime*_bulletSpeed);

        if (transform.position.y > 10 || transform.position.y < -10 ||(Vector3.Distance(transform.position, _target) < 0.1f))
            Dead();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        
        if(damageable != null  && collider.tag != "Enemy")
        {
            damageable.Damage(_bulletDamage);
            Dead();
        }
    }

    protected override void Dead()
    {
        Destroy(gameObject);
    }

}
