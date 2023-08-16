using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinNewPlayer : MonoBehaviour
{
    [SerializeField] private PlayersController _playersController;
    [SerializeField] private GameObject _player;


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
        DontDestroyOnLoad(playerInput.gameObject);

    }

    private void OnPlayerJoined(GameObject playerInput)
    {
        _amountPlayers++;
        _playersController.AddPlayer(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);

    }
}
