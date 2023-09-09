using UnityEngine;

public class SpeedEnemy : Enemy
{
    private float sinCenterY;

    private float _amplitude;
    private float _frequency;
    private bool _isNegative = false;

    public void SetSin(float amplitude,float frequency,bool isNegative){
        _amplitude = amplitude;
        _frequency = frequency;
        _isNegative = isNegative;
    }

    public override void Start() {
        base.Start();
        sinCenterY = transform.position.x;
      
    }

    private void Update() {
        DoMove();
    }

    public void DoMove(){
        Vector2 pos = transform.position;

        float sin = Mathf.Sin((pos.y*_amplitude)/_frequency);
        if(_isNegative)
            sin *=-1;
        pos.x = sinCenterY + sin;

        transform.position= pos;


        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;


        if(transform.position.y < -6)
           Destroy(this.gameObject);
    }


    protected override void Dead(){
        base.Dead();
        transform.GetComponentInParent<SpeedShipController>().SpeedShipDead();
    }
}
