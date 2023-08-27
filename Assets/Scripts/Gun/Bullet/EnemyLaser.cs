using UnityEngine;

public class EnemyLaser : Ammo
{ 
    [SerializeField] private int _bulletSpeed=8;
    [SerializeField] private int _bulletDamage=1;

    public void SetDamageAndSpeed(int speed,int damage){
        if(damage >0)
            _bulletDamage = damage;
        if(speed >0)
            _bulletSpeed = speed;
    }
    
    private void Start(){
        _bulletSpeed *= -1;
    }

    void Update(){
      DoMove();
    }


    public override void DoMove(){
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (transform.position.y > 10 || transform.position.y < -10)
            Dead();
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null && collider.tag =="Player")
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
