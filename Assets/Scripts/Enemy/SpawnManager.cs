using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public event UnityAction<int> enemyKilledEvent;


    [SerializeField] private GameObject[] _enemyPrefab;

    [SerializeField] private int _spawnRate;


    [SerializeField] private List<GameObject> _enemyList;
    private IEnumerator _spawnEnemyRoutine;

    private int _spawnCount=0;

    private void Start()
    {
        _spawnEnemyRoutine = SpawnEnemy();

        StartCoroutine(_spawnEnemyRoutine);
    }

    public void PlayerDeath()
    {
        StopCoroutine(_spawnEnemyRoutine);
    }

    private IEnumerator SpawnEnemy()
    {
        //transform.rotation * Quaternion.Euler(0f,0f, Random.Range(-360, 360))
        while (true) 
        {
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            int randomEnemy = Random.Range(0, _enemyPrefab.Length);
            GameObject enemy =  Instantiate(_enemyPrefab[randomEnemy], randomPosition, Quaternion.identity, transform);
            enemy.name = enemy.name + _spawnCount;
            _spawnCount++;
            _enemyList.Add(enemy);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void KilledEnemy(int score, GameObject enemy)
    {
        if (enemy == null)
            Debug.Log("Test");
        enemyKilledEvent?.Invoke(score);
        int enemyNumber = _enemyList.FindIndex(obj => obj.name == enemy.name);
        if (enemyNumber != -1)
            _enemyList.RemoveAt(enemyNumber);
    }

    public GameObject GetRandomObject()
    {
        int random = Random.Range(0, _enemyList.Count);
        return _enemyList[random];
    }

}
