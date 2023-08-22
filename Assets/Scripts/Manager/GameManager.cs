using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private GameObject _pauseMenu;



    [SerializeField] private Score _score;

    private bool _isAllPlayerDied=false;

    async  private void Start() {
        await UnityServices.InitializeAsync();
    }

    public void AllPlayerDead()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "levelNumber", SceneManager.GetActiveScene().name},
        };

        
        AnalyticsService.Instance.CustomData("PlayerDead",parameters);
        _gameOverScreen.SetActive(true);

        spawnManager.PlayerDeath();

        _score.OpenRecordMenu();

        _isAllPlayerDied = true;
    }

    public void RestartGame()
    {
        if (_isAllPlayerDied  && _score.MenuOpen())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        _pauseMenu.GetComponent<Animator>().SetTrigger("Activate Pause menu");
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
