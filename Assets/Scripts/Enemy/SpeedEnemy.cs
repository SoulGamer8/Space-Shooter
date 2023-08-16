using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    private float sinCenterY;

    private float _amplitede;
    private float _frequency;
    private bool _isNegetive = false;

    public void SetSin(float amplitede,float frequency,bool isNegetive){
        _amplitede = amplitede;
        _frequency = frequency;
        _isNegetive = isNegetive;
    }

    private void Start() {
        sinCenterY = transform.position.x;
      
    }

    private void Update() {
        DoMove();
    }



    public override void Damege(int damage)
    {
        base.Damege(damage);
    }

  
    protected override void DoMove()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin((pos.y*_amplitede)/_frequency);
        if(_isNegetive)
            sin *=-1;
        pos.x = sinCenterY + sin;

        transform.position= pos;


        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;


        if(transform.position.y < -6)
            Dead();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(collider.tag== "Player"){
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void Dead()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    protected override void DoShoot()
    {
        throw new System.NotImplementedException();
    }

}
