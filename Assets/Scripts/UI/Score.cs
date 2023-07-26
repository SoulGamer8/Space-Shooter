using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [SerializeField] private GameObject _newBestScoreMenu;

    private bool _newRecord = false;

    [SerializeField] private int _score;
    [SerializeField] private int _bestScore= 0;

    private bool _isMenuOpen = false;

    private void OnEnable()
    {
        _spawnManager.enemyKilledEvent += AddScore;
    }

    private void OnDisable()
    {
        _spawnManager.enemyKilledEvent -= AddScore;
    }

    private void Start()
    {
        _scoreText = this.GetComponent<TextMeshProUGUI>();
        LoadNumber();
        UpdateUI();
        
    }

    private void UpdateUI()
    {
        _scoreText.text ="Score: " +  _score.ToString();
        _bestScoreText.text = "Best Score: " + _bestScore.ToString();
    }

    private void AddScore(int score)
    {
        _score += score;
        if (_score > _bestScore)
        {
            _bestScore = _score;
            SaveNumber();
            _newRecord = true;
        }
        UpdateUI();
    }


    public void OpenRecordMenu()
    {
        if(_newRecord )
        {
            _newBestScoreMenu.SetActive(true);
         
        }
    }

    public bool MenuOpen()
    {
        return !_newBestScoreMenu.gameObject.activeSelf;
    }

    private void SaveNumber()
    {
        PlayerPrefs.SetInt("bestScore", _bestScore);
    }

    public void LoadNumber()
    {
        _bestScore = PlayerPrefs.GetInt("bestScore");
        UpdateUI();
    }
}
