using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private int _bulletDamage=1;

    private  void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null && collider.tag =="Player")
            damageable.Damage(_bulletDamage);
    }
}
