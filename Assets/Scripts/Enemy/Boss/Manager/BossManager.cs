using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private AudioClip _bossSpawnAudio;
    [SerializeField] private GameObject _bossHealthBar;


    private AudioSource _audioSource;
    private void Start() {
        _audioSource= GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_bossSpawnAudio);
        StartCoroutine(SpawnBossCoroutine());
    }


    IEnumerator SpawnBossCoroutine(){
        yield return new WaitForSeconds(_bossSpawnAudio.length-1);
        Instantiate(_boss);
    }


}
