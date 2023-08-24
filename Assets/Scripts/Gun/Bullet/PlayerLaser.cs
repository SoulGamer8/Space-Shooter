using UnityEngine;

public class PlayerLaser : Ammo
{
    private float _bulletSpeed= 8f;
    private int _bulletDamage = 1;
    private bool _trippleShootIsActive = false;
    private float _trippleShootSpeed;

    public void SetDamageAndSpeed(int damage,float speed){
        if(damage >0)
            _bulletDamage = damage;
        if(speed >0)
            _bulletSpeed = speed;
    }
    

    private void Update() {
        DoMove();
    }

    public void ActiveTrippleShot(){
        _trippleShootIsActive = true;
    }

    public override void DoMove(){
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (_trippleShootIsActive)
        {
            transform.position += new Vector3(_trippleShootSpeed, _bulletSpeed, 0) * Time.deltaTime;
        }

        if (transform.position.y > 10 || transform.position.y < -10)
            Dead();
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null  && collider.tag =="Enemy")
        {
           
            damageable.Damege(_bulletDamage);
            Dead();
        }
    }

    protected override void Dead(){
        if (transform.parent != null)
        {
           if (gameObject.transform.parent.transform.childCount == 1 )
                Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

}
