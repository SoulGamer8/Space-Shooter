using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [SerializeField] private GameObject _newBestScoreMenu;

    [SerializeField] private bool _isInfinityLevel= false;

    [SerializeField] private int _score;
    [SerializeField] private int _bestScore= 0;


    private void OnEnable()
    {
        _spawnManager.enemyKilledEvent += AddScore;
    }

    private void OnDisable()
    {
        _spawnManager.enemyKilledEvent -= AddScore;
    }

    private void Start(){
        _scoreText = this.GetComponent<TextMeshProUGUI>();
        LoadNumber();
        UpdateUI();
        if(_isInfinityLevel)
            _bestScoreText.text = "Best Score: " + _bestScore.ToString();
    }

    private void UpdateUI(){
        _scoreText.text ="Score: " +  _score.ToString();
   
    }

    private void AddScore(int score){
        _score += score;
        UpdateUI();
    }

    public void OpenRecordMenu(){
        if (_score > _bestScore){
            _bestScore = _score;
            SaveNumber();
            _newBestScoreMenu.SetActive(true);
        }
    }

    public bool IsNewRecordMenuOpen(){
        return !_newBestScoreMenu.gameObject.activeSelf;
    }

    private void SaveNumber(){
        PlayerPrefs.SetInt("bestScore", _bestScore);
    }

    public void LoadNumber(){
        _bestScore = PlayerPrefs.GetInt("bestScore");
        UpdateUI();
    }
}
