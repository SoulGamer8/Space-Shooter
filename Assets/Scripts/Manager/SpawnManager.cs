using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public event UnityAction<int> enemyKilledEvent;

    [SerializeField] private bool _isSpawnEnemy=true;

    [SerializeField] private GameObject[] _enemyPrefab;

    [SerializeField] private int _spawnRate;


    [SerializeField] private List<GameObject> _enemyList;


    [Header("Coin")]
    [SerializeField] private bool _isSpawnCoin = false;

    [SerializeField] private GameObject _coinPrefab;

    [Range(0f,1f)]
    [SerializeField] private float _chanceSpawnCoin;


    private IEnumerator _spawnEnemyRoutine;

    private int _spawnCount=0;

  

    private void Start()
    {

        if(_isSpawnEnemy){
            _spawnEnemyRoutine = SpawnEnemy();
            StartCoroutine(_spawnEnemyRoutine);
        }
    }

    public void PlayerDeath()
    {
        StopCoroutine(_spawnEnemyRoutine);
    }

    public void KilledEnemy(int score, GameObject enemy)
    {
        enemyKilledEvent?.Invoke(score);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            int randomEnemy = Random.Range(0, _enemyPrefab.Length);
            Instantiate(_enemyPrefab[randomEnemy], randomPosition, Quaternion.identity, transform);
        
            float chance = Random.Range(0f, 1f);
            if (chance >= _chanceSpawnCoin && _isSpawnCoin)
                Instantiate(_coinPrefab, randomPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

}
