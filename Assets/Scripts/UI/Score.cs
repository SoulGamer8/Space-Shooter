using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager;

    private TextMeshProUGUI _scoreTesxt;

    private int _score;

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
        _scoreTesxt = this.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _scoreTesxt.text ="Score: " +  _score.ToString();
    }

    private void AddScore(int score)
    {
        _score += score;
        UpdateUI();
    }
}
