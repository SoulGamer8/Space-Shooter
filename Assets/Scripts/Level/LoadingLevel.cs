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
    private Texture _imageLock;


    private void Start(){
        _imageLock = GetComponent<RawImage>().texture;

        int isOpen = PlayerPrefs.GetInt(this.gameObject.name);
        if (isOpen== 1)
            _isOpen = true;
        if (_isOpen)
            ChangeImage(_openLevelTexture);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (_isOpen)
            OpenMenu();
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (_isOpen){
            _menu.SetActive(false);
            _startLevel.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    }

    private void OpenMenu(){
        _menu.SetActive(true);
        _startLevel = _menu.transform.GetChild(0);
        _startLevel.GetComponent<Button>().onClick.AddListener(() => _loadingScene.LoadScene(_LevelID));
    }


    private void ChangeImage(Texture image){
        this.gameObject.GetComponent<RawImage>().texture = image;
    }

    public void OpenLevel()
    {
        _isOpen = true;
        ChangeImage(_openLevelTexture);
        PlayerPrefs.SetInt(this.gameObject.name, 1);
    }

    public void CloseLevel(){
        _isOpen = false;
        ChangeImage(_imageLock);
        PlayerPrefs.SetInt(this.gameObject.name, 0);
    }
}
