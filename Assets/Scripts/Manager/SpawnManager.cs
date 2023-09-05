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


    [SerializeField]  private int _sumScore;

    private void Start(){
        if(_isEnemySpawn)
            StartCoroutine(SpawnEnemyCoroutine());
        if(_isPowerUpSpawn)
            StartCoroutine(SpawnPowerUpCoroutine());
    }

    public void PlayerDeath(){
        StopAllCoroutines();
    }

    public void KilledEnemy(int score, GameObject enemy){
        enemyKilledEvent?.Invoke(score);
        _sumScore +=score;
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
        while (true)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            GameObject randomEnemy = ChooseWeightedItem(_enemiesArray);
            Instantiate(randomEnemy, randomPosition, Quaternion.identity, transform);

            float chance = Random.Range(0f, 1f);
            if (chance >= _chanceSpawnCoin && _isSpawnCoin)
                Instantiate(_coinPrefab, randomPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private IEnumerator SpawnPowerUpCoroutine(){
        while(true){
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            yield return new WaitForSeconds(_timeSpawnPowerUp);
            Instantiate(ChooseWeightedItem(_powerUpsArray),randomPosition,Quaternion.identity);
        }
    }

}
