using UnityEngine;

public class PowerUpWeightController : MonoBehaviour
{
    public static PowerUpWeightController instance;

    [SerializeField] private GameObject[] _powerUp;

    private void Awake() {
         if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ChangeSpawnChanceWeightAmmo(int weight){
        _powerUp[0].GetComponent<PowerUp>().ChangeSpawnChanceWeight(weight);
    }

    public void ChangeSpawnChanceWeightRepair(int weight){
        _powerUp[1].GetComponent<PowerUp>().ChangeSpawnChanceWeight(weight);
    }

    public void ChangeSpawnChanceWeightRespawn(int weight){
        _powerUp[2].GetComponent<PowerUp>().ChangeSpawnChanceWeight(weight);
    }

    public void OnNotify(object value)
    {
        Debug.Log(value);
    }
}
