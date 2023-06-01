using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;


    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(4);
        while (true) 
        { 
            Instantiate(_enemyPrefab, new Vector3(Random.Range(-5, 5), transform.position.y, 0), Quaternion.identity); 
        }
        
    }
}
