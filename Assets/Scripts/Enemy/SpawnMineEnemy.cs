using System.Collections;
using UnityEngine;

public class SpawnMineEnemy : Enemy
{
   
    [Header("Mine Settings")]
    [SerializeField] private GameObject _minePrefab;
    [SerializeField] private float _timeSpawn;
    private void Awake() {
        Shoot();
    }

    private void Update() {
        DoMove();
    }

    public void DoMove(){
        transform.position +=new Vector3(0,-1,0)*Time.deltaTime *_speed;
        if(transform.position.y<-6)
            Destroy(this.gameObject);
    }

    public void Shoot(){
        StartCoroutine(SpawnMine());
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
