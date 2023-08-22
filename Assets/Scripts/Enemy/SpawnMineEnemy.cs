using System.Collections;
using UnityEngine;

public class SpawnMineEnemy : Enemy
{
   
    [Header("Mine Settings")]
    [SerializeField] private GameObject _minePrefab;
    [SerializeField] private float _timeSpawn;
    private void Awake() {
        DoShoot();
    }

    private void Update() {
        DoMove();
    }

    public override void Damege(int damage){
        _health -=damage;
        if(_health <0)
            Dead();
    }

    protected override void DoMove(){
        transform.position +=new Vector3(0,-1,0)*Time.deltaTime *_speed;
        if(transform.position.y<-6)
            Dead();
    }

    protected override void DoShoot(){
        StartCoroutine(SpawnMine());
    }

    protected override void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(collider.tag== "Player"){
            damageable.Damege(_damage);
            Dead();
        }
    }

    protected override void Dead(){
        Destroy(gameObject);
    }

    private IEnumerator SpawnMine(){
        while(true){
            Instantiate(_minePrefab,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(_timeSpawn);
        }
    }

    public override int GetSpawnChanceWeight(){
        return _spawnChanceWeight;
    }
}
