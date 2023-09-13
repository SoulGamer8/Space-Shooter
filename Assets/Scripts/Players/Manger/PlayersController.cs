using UnityEngine;
public class PlayersController : MonoBehaviour
{

    [SerializeField] private JoinNewPlayer _joinNewPlayer;
    [SerializeField] private GameManager _gameManager;
    
    private LevelProgressManager _levelProgressManager;
    private int _playerAlive = 0;

    private void Awake() {
        GameObject levelProgressManager;
        levelProgressManager = GameObject.FindGameObjectWithTag("LevelManager");
        if(levelProgressManager != null)
            _levelProgressManager=levelProgressManager.GetComponent<LevelProgressManager>();
    }

    public void PlayerDead(){
        _playerAlive--;
        if (_playerAlive == 0){
            _gameManager.AllPlayerDead();
            _levelProgressManager.AllPlayerDead();
        }

    }

    public void AddPlayer(GameObject player){
        _playerAlive++;
        player.GetComponent<PlayerHealth>().Spawn(gameObject.GetComponent<PlayersController>());
    }



    public void ReSpawnPlayer(){
        if (_playerAlive < 2){
            _joinNewPlayer.RespawnPlayer();
            _playerAlive++;
        }
    }

}
