using UnityEngine;

public class SpawnNewPlayer : MonoBehaviour
{
    [SerializeField] private JoinNewPlayer joinNewPlayer;

    public static int _amountPlayers=1;

    private void Awake() {
        joinNewPlayer = GetComponent<JoinNewPlayer>();
        joinNewPlayer._amountPlayers = _amountPlayers;
        Debug.Log(_amountPlayers);
    }
}
