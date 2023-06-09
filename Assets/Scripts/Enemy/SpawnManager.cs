using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public event UnityAction<int> enemyKilledEvent;


    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private PlayerHealth _playerHealth;

    private IEnumerator _spawnEnemyRoutine;

    private void OnEnable()
    {
        _playerHealth.PlayerDieEvent += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _playerHealth.PlayerDieEvent -= OnPlayerDeath;
    }

    private void Start()
    {
        _spawnEnemyRoutine = SpawnEnemy();

        StartCoroutine(_spawnEnemyRoutine);
    }

    private void OnPlayerDeath()
    {
        StopCoroutine(_spawnEnemyRoutine);
    }

    private IEnumerator SpawnEnemy()
    {
      
        while (true) 
        {
            Vector3 randomPosition = new Vector3(Random.Range(-8, 8), transform.position.y, 0);
            Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(4);
        }
    }

    public void KilledEnemy(int score)
    {
        enemyKilledEvent?.Invoke(score);
    }

}