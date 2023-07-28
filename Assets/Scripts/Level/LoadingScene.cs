using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScene;
    [SerializeField] private Slider _slider;


    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
       AsyncOperation scene = SceneManager.LoadSceneAsync(sceneId);
        _loadingScene.SetActive(true);
        _slider = _loadingScene.transform.GetChild(0).gameObject.GetComponent<Slider>();
        while (!scene.isDone)
        {
            float progressValue = Mathf.Clamp01(scene.progress / 0.9f);
            _slider.value = progressValue;

            yield return null;
        }

    }

}
