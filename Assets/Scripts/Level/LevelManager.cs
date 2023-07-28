using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;


    public static bool _isWin;

    public static string _scene;

    private void Start()
    {
        LevelComplete();
    }

    private void LevelComplete()
    {
        int levelNumber = _levels.FindIndex(obj => obj.name == _scene);
        _levels[levelNumber+1].GetComponent<LoadingLevel>().OpenLevel();
    }
}
