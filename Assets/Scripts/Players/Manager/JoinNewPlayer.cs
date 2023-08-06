using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinNewPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;

    [SerializeField] private PlayersController _playersController;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _player;

    [SerializeField] private Rect[] _playerTransform;
    [SerializeField] private Transform[] _healthBarTransform;

    private PlayerInputManager _playerInputManager;
     private int _amountPlayers=-1;


    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void RespawnPlayer()
    {
        _playerInputManager.playerPrefab = _player;
        _playerInputManager.JoinPlayer();
        OnPlayerJoined(_player);
    }

    private  void OnPlayerJoined(PlayerInput playerInput)
    {
        _amountPlayers++;
        _playersController.AddPlayer(playerInput.gameObject);
        CreateHealthBar(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);

    }

    private void OnPlayerJoined(GameObject playerInput)
    {
        _amountPlayers++;
        _playersController.AddPlayer(playerInput.gameObject);
        CreateHealthBar(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);

    }

    private void CreateHealthBar(GameObject player)
    {
        GameObject healthBar = Instantiate(_healthBar, _playerTransform[_amountPlayers].position, Quaternion.identity, _canvas.transform.transform);

        player.GetComponent<PlayerHealth>().AddHealthBar(healthBar);
    }


    private void OnDrawGizmos() {
        Texture texture = _healthBar.GetComponent<RawImage>().texture;
        Gizmos.DrawGUITexture(_playerTransform[0], texture);
        Gizmos.DrawGUITexture(_playerTransform[1], texture);
    }
}
