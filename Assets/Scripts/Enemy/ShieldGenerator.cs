
using UnityEngine;

public class ShieldGenerator : Enemy
{
    [SerializeField] private Vector3 _targetPosotion;
    [SerializeField] private float _angel;
    [SerializeField] private GameObject _beam;

    public override int GetSpawnChanceWeight(){return 0;}

    private void Awake() {
        _beam = gameObject.transform.GetChild(0).gameObject;
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        damageable.Damege(1);
    }

    private void Update(){
        DoMove();
    }

    public override void DoMove(){
        transform.position = Vector3.MoveTowards(transform.position,_targetPosotion,_speed*Time.deltaTime);
        if(Vector3.Distance(transform.position,_targetPosotion)< 0.1f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,_angel),0.1f);
        if(transform.rotation ==  Quaternion.Euler(0,0,_angel))
            Shoot();

    }
    
    public override void Shoot(){
        _beam.SetActive(true);
    }

    public override void Damege(int damage){
        _health -= damage;
        if(_health < 0)
            Dead();
    }


    protected override void Dead(){
        Destroy(gameObject);
    }

}
