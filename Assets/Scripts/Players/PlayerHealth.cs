using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public event UnityAction PlayerDieEvent;
    
    [SerializeField] private int _health;



    [Header("Shield")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private int _shieldHealth;
    [SerializeField] private float _timeWhenActiveShild;

    private int _curentlyShieldHealth;
    private bool _isShieldActivate;
    private GameObject _sheild;
    private void Dead()
    {
        PlayerDieEvent?.Invoke();
        Destroy(gameObject); 
    }

    public void TakeDamage(int damage)
    {
        if (_isShieldActivate)
        {
            _curentlyShieldHealth -= damage;
           
            if (_curentlyShieldHealth <= 0)
            {
                Destroy(_sheild);
                _isShieldActivate = false;
            }
          
        }
           
        else
            _health -= damage;

        if (_health <=0)
            Dead();
    }

    public void ActivateShild()
    {
        _curentlyShieldHealth = _shieldHealth;
        StartCoroutine(Sheild());
        _sheild = Instantiate(_shield, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity,transform);
        _isShieldActivate = true;
    }


    private IEnumerator Sheild()
    {
        yield return new WaitForSeconds(_timeWhenActiveShild);
        _isShieldActivate = false;
        Destroy(_sheild);
    }

}
