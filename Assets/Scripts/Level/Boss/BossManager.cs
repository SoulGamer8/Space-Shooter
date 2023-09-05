using System.Collections;
using TMPro;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private AudioClip _bossSpawnAudio;
    [SerializeField] private AudioClip _alarmSound;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject _completeLevelMenu;
    [SerializeField] private StarsController _starsController;
        
    private AudioSource _audioSource;
    private int _starAmmount=3;
    public void SetStar(){
        _starsController.SetStar(_starAmmount);
    }

    private void Start(){
        _audioSource= GetComponent<AudioSource>();
        StartCoroutine(TextSpawnBossCoroutine());
    }

    private void SpawnBoss(){
        _audioSource.PlayOneShot(_bossSpawnAudio);
        StartCoroutine(SpawnBossCoroutine());
    }

    public void LevelComplete(){
        _completeLevelMenu.SetActive(true);
        SetStar();
    }

    private IEnumerator TextSpawnBossCoroutine(){
        _audioSource.PlayOneShot(_alarmSound);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        SpawnBoss();

    }

    private IEnumerator SpawnBossCoroutine(){
        yield return new WaitForSeconds(_bossSpawnAudio.length-1);
        Instantiate(_boss);
        text.gameObject.SetActive(false);
        StartCoroutine(TimeBossFightCoroutine());
    }

    private IEnumerator TimeBossFightCoroutine(){
        yield return new WaitForSeconds(120);
        _starAmmount --;
        Debug.Log(_starAmmount);
    }

}