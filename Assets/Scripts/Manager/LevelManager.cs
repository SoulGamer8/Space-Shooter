using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;


    public static bool _isWin;

    public static Scene _scene;

    private void Start()
    {
        Debug.Log(_isWin);
        Debug.Log(_scene.name);
    }

    private void LevelComplete()
    {
        int levelNumber = _levels.FindIndex(obj => obj.name == _scene.name);
        _levels[levelNumber].GetComponent<LoadingLevel>().OpenLevel();
    }
}
