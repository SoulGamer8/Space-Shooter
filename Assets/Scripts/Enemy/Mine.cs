using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _timeToSelfDestroy;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        StartCoroutine(SelfDestroy());
    }


    private void OnTriggerEnter2D(Collider2D collider) {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(collider.tag== "Player"){
            damageable.Damege(_damage);
            Dead();
        }
    }


    private void Dead(){
        _animator.SetTrigger("IsDead");

        Destroy(gameObject, 0.2f);
    }


    private IEnumerator SelfDestroy(){
        yield return new WaitForSeconds(_timeToSelfDestroy);
        Dead();
    } 

}
