using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ChangeSpawnChacneWeightAmmo(int weight){
        _powerUp[0].GetComponent<PowerUp>().ChangeSpawnChacneWeight(weight);
    }

    public void ChangeSpawnChacneWeightRepair(int weight){
        _powerUp[1].GetComponent<PowerUp>().ChangeSpawnChacneWeight(weight);
    }

    public void ChangeSpawnChacneWeightRespawn(int weight){
        _powerUp[2].GetComponent<PowerUp>().ChangeSpawnChacneWeight(weight);
    }
}
