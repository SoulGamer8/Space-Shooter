using UnityEngine;
using UnityEngine.InputSystem;

public class JoinNewPlayer : MonoBehaviour
{
    [SerializeField] private PlayersController _playersController;
    [SerializeField] private GameObject _player;


    private PlayerInputManager _playerInputManager;
    public int _amountPlayers=0;


    private void Start(){
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void RespawnPlayer(){
        _playerInputManager.playerPrefab = _player;
        _playerInputManager.JoinPlayer();
        OnPlayerJoined(_player);
    }

    private  void OnPlayerJoined(PlayerInput playerInput){
        _amountPlayers++;
        _playersController.AddPlayer(playerInput.gameObject);
        SpawnNewPlayer._amountPlayers = _amountPlayers;
    }

    private void OnPlayerJoined(GameObject playerInput){
        _amountPlayers++;
        _playersController.AddPlayer(playerInput.gameObject);
    }
}
