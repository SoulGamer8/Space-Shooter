using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using TMPro;
public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] private bool _isStandartLevel=true;

    [ConditionalHide("_isStandartLevel", true)]
    [SerializeField] private float _timeHowLongLive;


    [SerializeField] private bool _isBossLevel=true;
    
    [ConditionalHide("_isHasBoss", true)]
    [SerializeField] private GameObject _boss;

    [ConditionalHide("_isHasBoss", true)]
    [SerializeField] private AudioClip _bossSpawnAudio;

    [ConditionalHide("_isHasBoss", true)]
    [SerializeField] private AudioClip _alarmSound;
    
    [ConditionalHide("_isHasBoss", true)]
    [SerializeField] private TextMeshProUGUI text;


    [Header("Level Complete")]
    [SerializeField] private GameObject _completeLevelMenu;
    [SerializeField] private StarsController _starsController;
    private int _starAmmount = 3;

    private WalletManager _walletManager;
    private AudioSource _audioSource;
    private bool _isPlayerTakeDamage =false;

    private void Start(){
        _walletManager = WalletManager.instance;
        _audioSource =GetComponent<AudioSource>();
        if(_isStandartLevel)
            StartCoroutine(TimerToCompleteLevelCoroutine());
        if(_isBossLevel)
            StartBossFight();
    }

    public void SetStar(){
        _starsController.SetStar(_starAmmount);
    }

    #region Standart Level
    private IEnumerator TimerToCompleteLevelCoroutine(){
        yield return new WaitForSeconds(_timeHowLongLive);
        LevelComplete();
    } 
    #endregion

    #region Boss
    private void StartBossFight(){
        _audioSource.PlayOneShot(_bossSpawnAudio);
        StartCoroutine(TextSpawnBossCoroutine());
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

    private void SpawnBoss(){
        _audioSource.PlayOneShot(_bossSpawnAudio);
        StartCoroutine(SpawnBossCoroutine());
    }

    private IEnumerator TimeBossFightCoroutine(){
        yield return new WaitForSeconds(120);
        _starAmmount --;
    }
    #endregion
    
    public void LevelComplete(){
        _completeLevelMenu.SetActive(true);
        SetStar();
        
        LevelManager._isWin = true;
        LevelManager._scene = SceneManager.GetActiveScene().name;

        _walletManager.SaveMoney();
        Analytics();
        Debug.Log("Level complete");
    }


    private void Analytics(){
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
        { "level", SceneManager.GetActiveScene().name},
        };

        AnalyticsService.Instance.CustomData("levelWin",parameters);
    }

    public void PlayerTakeDamage(){
        if(!_isPlayerTakeDamage){
            _isPlayerTakeDamage = true;
            _starAmmount--;
        }
    }
}
