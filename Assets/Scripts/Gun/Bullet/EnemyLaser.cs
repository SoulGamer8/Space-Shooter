using UnityEngine;

public class EnemyLaser : Ammo
{ 
    private void Start(){
        _bulletSpeed *= -1;
    }

    void FixedUpdate(){
      DoMove();
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null && collider.tag =="Player")
        {
            damageable.Damage(_bulletDamage);
            Dead();
        }
    }

}
