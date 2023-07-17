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
    private int _score;
    
    private int _bestScore= 0;


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
        if(_score > _bestScore)
            _bestScore = _score;
        UpdateUI();
        SaveNumber();
    }

    public void SaveNumber()
    {
        PlayerPrefs.SetInt("bestScore", _bestScore);
    }

    public void LoadNumber()
    {
        _bestScore = PlayerPrefs.GetInt("bestScore");
        UpdateUI();
    }
}
