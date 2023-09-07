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

    public override void Start() {
        base.Start();
        sinCenterY = transform.position.x;
      
    }

    private void Update() {
        DoMove();
    }

    public void DoMove(){
        Vector2 pos = transform.position;

        float sin = Mathf.Sin((pos.y*_amplitede)/_frequency);
        if(_isNegetive)
            sin *=-1;
        pos.x = sinCenterY + sin;

        transform.position= pos;


        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;


        if(transform.position.y < -6)
           Destroy(this.gameObject);
    }

    public override int GetSpawnChanceWeight(){
       return _spawnChanceWeight;
    }
}
