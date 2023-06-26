using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public event UnityAction<int> enemyKilledEvent;


    [SerializeField] private GameObject[] _enemyPrefab;

    private IEnumerator _spawnEnemyRoutine;


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
            Instantiate(_enemyPrefab[randomEnemy], randomPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(4);
        }
    }

    public void KilledEnemy(int score)
    {
        enemyKilledEvent?.Invoke(score);
    }

}
