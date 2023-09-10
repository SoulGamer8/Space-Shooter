using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class Score : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager;

    [SerializeField] private LocalizedString localizedStringScore;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [SerializeField] private GameObject _newBestScoreMenu;

    [SerializeField] private bool _isInfinityLevel= false;

    private int _score;
    private int _bestScore= 0;




    private void OnEnable(){
        _spawnManager.enemyKilledEvent += AddScore;

        localizedStringScore.Arguments = new object[] {_score};
        localizedStringScore.StringChanged += UpdateUI;
    }

    private void OnDisable(){
        _spawnManager.enemyKilledEvent -= AddScore;
        localizedStringScore.StringChanged -= UpdateUI;
    }

    private void Start(){
        _scoreText = this.GetComponent<TextMeshProUGUI>();
        LoadNumber();
        if(_isInfinityLevel)
            _bestScoreText.text = "Best Score: " + _bestScore.ToString();
    }

    private void UpdateUI(string value){
        _scoreText.text =value;
   
    }

    private void AddScore(int score){
        _score += score;
        localizedStringScore.Arguments[0] = _score;
        localizedStringScore.RefreshString();
    }

    public void OpenRecordMenu(){
        if (_score > _bestScore){
            _bestScore = _score;
            SaveNumber();
            _newBestScoreMenu.SetActive(true);
        }
    }

    public bool IsNewRecordMenuOpen(){
        if(!_isInfinityLevel)
            return false;
        return !_newBestScoreMenu.gameObject.activeSelf;
    }

    private void SaveNumber(){
        PlayerPrefs.SetInt("bestScore", _bestScore);
    }

    public void LoadNumber(){
        _bestScore = PlayerPrefs.GetInt("bestScore");
    }
}
