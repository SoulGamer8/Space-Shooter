using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using TMPro;
public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] private bool _isStandardLevel=true;

    [ConditionalHide("_isStandardLevel", true)]
    [SerializeField] private float _timeHowLongLive;
    [SerializeField] private int _percentForSecondStar;
    [SerializeField] private int _percentForThirdStar;

    [SerializeField] private bool _isBossLevel = false;
    
    [ConditionalHide("_isBossLevel", true)]
    [SerializeField] private GameObject _boss;

    [ConditionalHide("_isBossLevel", true)]
    [Header("Audio")]
    [SerializeField] private AudioClip _bossSpawnAudio;
    [ConditionalHide("_isBossLevel", true)]
    [SerializeField] private AudioClip _bossSpawnAudioEaster;

    [ConditionalHide("_isBossLevel", true)]
    [SerializeField] private AudioClip _alarmSound;
     
    [ConditionalHide("_isBossLevel", true)]
    [SerializeField] private TextMeshProUGUI text;


    [Header("Level Complete")]
    [SerializeField] private GameObject _completeLevelMenu;
    [SerializeField] private StarsController _starsController;
    [SerializeField] private int _starAmount = 3;



    private AudioSource _audioSource;
    private bool _isPlayerTakeDamage =false;
    private Coroutine _timerToCompleteLevelCoroutine;


    private void Start(){
        _audioSource =GetComponent<AudioSource>();
        if(_isStandardLevel)
            _timerToCompleteLevelCoroutine = StartCoroutine(TimerToCompleteLevelCoroutine());
        if(_isBossLevel)
            StartBossFight();
    }

    #region Standard Level
    
    public void AllPlayerDead(){
        StopCoroutine(_timerToCompleteLevelCoroutine);
    }

    private IEnumerator TimerToCompleteLevelCoroutine(){
        yield return new WaitForSeconds(_timeHowLongLive);
        SetStarLevelStandard();
        LevelComplete();
    }

    private void SetStarLevelStandard(){
        SpawnManager spawnManager =  GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        int sumSpawnScore = spawnManager.GetSumSpawnScore();
        int  smuKilledEnemyScore = spawnManager.GetSumKilledEnemyScore();
        if(sumSpawnScore == 0)
            return;
        if((smuKilledEnemyScore * 100)/sumSpawnScore < _percentForSecondStar)
            _starAmount--;
         if((smuKilledEnemyScore * 100)/sumSpawnScore < _percentForThirdStar)
            _starAmount--;
    } 
    #endregion

    #region Boss
    private void StartBossFight(){
        StartCoroutine(TextSpawnBossCoroutine());
    }

    private IEnumerator TextSpawnBossCoroutine(){
        _audioSource.PlayOneShot(_alarmSound);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        PlayAudioSpawnBoss();
    }

    private void PlayAudioSpawnBoss(){
        AudioClip audioSpawnBoss = _bossSpawnAudio;

        float randomSound = Random.Range(0f,1f);
        if(randomSound < 1f)
            _audioSource.PlayOneShot(_bossSpawnAudioEaster);

        _audioSource.PlayOneShot(audioSpawnBoss);
        StartCoroutine(SpawnBossCoroutine(audioSpawnBoss));
    }

    private IEnumerator SpawnBossCoroutine(AudioClip audioSpawnBoss){
        yield return new WaitForSeconds(audioSpawnBoss.length-1);
        Instantiate(_boss);
        text.gameObject.SetActive(false);
        StartCoroutine(TimeBossFightCoroutine());
    }



    private IEnumerator TimeBossFightCoroutine(){
        yield return new WaitForSeconds(120);
        Debug.Log("test");
        _starAmount --;
    }
    #endregion


    public void LevelComplete(){
        _completeLevelMenu.SetActive(true);
        _starsController.SetStar(_starAmount);
        
        ControllerLevel._scene = SceneManager.GetActiveScene().name;

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
        if(!_isPlayerTakeDamage && _isBossLevel){
            _isPlayerTakeDamage = true;
            _starAmount--;
        }
    }
}
