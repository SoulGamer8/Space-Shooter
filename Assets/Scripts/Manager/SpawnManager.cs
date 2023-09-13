using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{

    public event UnityAction<int> enemyKilledEvent;

    [Header("Enemy")]
    [SerializeField] private bool _isEnemySpawn = true;
    [SerializeField] private GameObject[] _enemiesArray;
    [SerializeField] private int _spawnRate;

    [Header("PowerUp")]
    [SerializeField] private bool _isPowerUpSpawn= true;
    [SerializeField] private GameObject[] _powerUpsArray;
    [SerializeField] private float _timeSpawnPowerUp;
    [Header("Coin")]
    [SerializeField] private bool _isSpawnCoin = false;

    [SerializeField] private GameObject _coinPrefab;

    [Range(0f,1f)]
    [SerializeField] private float _chanceSpawnCoin;

    [SerializeField] private int _sumSpawnScore= 0 ;
    [SerializeField]  private int _sumKilledEnemyScore = 0;


    public int GetSumSpawnScore(){
        return _sumSpawnScore;
    }

    public int GetSumKilledEnemyScore(){
        return _sumKilledEnemyScore;
    }


    private void Start(){
        if(_isEnemySpawn)
            StartCoroutine(SpawnEnemyCoroutine());
        if(_isPowerUpSpawn)
            StartCoroutine(SpawnPowerUpCoroutine());
    }

    public void PlayerDeath(){
        StopAllCoroutines();
        GameObject[] enemies =GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0;i<enemies.Length;i++){
            Destroy(enemies[i]);
        }
    }

    public void KilledEnemy(int score){
        enemyKilledEvent?.Invoke(score);
        _sumKilledEnemyScore +=score;
    }

    private void AddSumScore(int score){
        _sumSpawnScore += score;
    }

    private GameObject ChooseWeightedItem(GameObject[] objects){
        int totalWeight = 0;
        int[] weight = new int[objects.Length];

        for(int i =0; i< objects.Length;i++){
            weight[i] = objects[i].GetComponent<ISpawnChanceWeight>().GetSpawnChanceWeight();
            totalWeight += weight[i];
        }

        int random = Random.Range(0,totalWeight);
        for(int i = 0; i < objects.Length; i++){
            if(random<weight[i])
                return objects[i];
            random -= weight[i];
        }
        return null;
    }

    private IEnumerator SpawnEnemyCoroutine(){
        yield return new WaitForSeconds(2);
        while (true)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            GameObject randomEnemy = ChooseWeightedItem(_enemiesArray);
            GameObject enemy =  Instantiate(randomEnemy, randomPosition, Quaternion.identity, transform);
            
            AddSumScore(enemy.GetComponent<IScore>().GetScore());
            SpawnCoin(randomPosition);

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private void SpawnCoin(Vector3 randomPosition){
        float chance = Random.Range(0f, 1f);
        if (chance >= _chanceSpawnCoin && _isSpawnCoin)
                Instantiate(_coinPrefab, randomPosition, Quaternion.identity, transform);
    }

    private IEnumerator SpawnPowerUpCoroutine(){
        while(true){
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            yield return new WaitForSeconds(_timeSpawnPowerUp);
            Instantiate(ChooseWeightedItem(_powerUpsArray),randomPosition,Quaternion.identity);
        }
    }

}
