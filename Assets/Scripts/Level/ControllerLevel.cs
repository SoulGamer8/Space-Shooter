using System.Collections.Generic;
using UnityEngine;

public class ControllerLevel : MonoBehaviour
{
    [SerializeField] private List<LoadingLevel> _levels;

    public static string _scene;

    private void Start() {
        LevelComplete();
    }

    public void LevelComplete(){
        int levelNumber = _levels.FindIndex(obj => obj.name == _scene);
        if(levelNumber != -1)
            _levels[levelNumber+1].GetComponent<LoadingLevel>().OpenLevel();
    }
}
