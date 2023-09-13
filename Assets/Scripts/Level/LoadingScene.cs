using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScene;
    [SerializeField] private Slider _slider;


    public void LoadScene(int sceneId){
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId){
       AsyncOperation scene = SceneManager.LoadSceneAsync(sceneId);
        _loadingScene.SetActive(true);
        _slider = _loadingScene.transform.GetChild(1).gameObject.GetComponent<Slider>();
        
        scene.allowSceneActivation = false;
        while (!scene.isDone){
            _slider.value = scene.progress ;
            if (scene.progress >= 0.9f){
                scene.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
