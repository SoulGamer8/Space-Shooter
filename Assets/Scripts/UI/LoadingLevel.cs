using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLevel : MonoBehaviour
{
    [SerializeField] private GameObject _menuPrefab;
    [SerializeField] private LoadingScene _loadingScene;
    [SerializeField] private int _LevelID;

    [SerializeField] private bool _isOpen = false;
    [SerializeField] private Texture _openLevelTexture;

    [SerializeField] private GameObject _menu;

    private Transform _startLevel;



    private void Start()
    {
        int test = PlayerPrefs.GetInt(this.gameObject.name);
        if (test== 1)
            _isOpen = true;
        if (_isOpen)
            ChangeImage();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isOpen)
            OpenMenu();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _menu.SetActive(false);
    }

    private void OpenMenu()
    {
        _menu.SetActive(true);
        _startLevel = _menu.transform.GetChild(0);
        _startLevel.GetComponent<Button>().onClick.AddListener(() => _loadingScene.LoadScene(_LevelID));
    }


    private void ChangeImage()
    {
        this.gameObject.GetComponent<RawImage>().texture = _openLevelTexture;
    }

    public void OpenLevel()
    {
        _isOpen = true;
        ChangeImage();
        PlayerPrefs.SetInt(this.gameObject.name, 1);
    }
}
