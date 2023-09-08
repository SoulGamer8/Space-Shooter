using UnityEngine;

public class PlayerLaser : Ammo
{
    private void Update() {
        DoMove();
    }

    public override void DoMove(){
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (transform.position.y > 10 || transform.position.y < -10)
            Dead();
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null  && collider.tag =="Enemy")
        {
            damageable.Damage(_bulletDamage);
            Dead();
        }
    }

}
