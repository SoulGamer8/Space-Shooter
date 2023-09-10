using System.Collections.Generic;
using UnityEngine;

public class AmmoMissile : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> _ammo;

    private int _currentlyMissile;
    private PowerUpWeightController _powerUpWeightController;
    private void Awake(){
        _powerUpWeightController = PowerUpWeightController.instance;
        _currentlyMissile = this.gameObject.transform.childCount;
        for(int i = 0;i<this.gameObject.transform.childCount;i++){
            _ammo.Add(this.gameObject.transform.GetChild(i).gameObject);
        }
    }

    public bool UseMissile(){
        if(_currentlyMissile>0){
            _ammo[_currentlyMissile-1].gameObject.SetActive(false);
            _currentlyMissile--;
            // _powerUpWeightController.ChangeSpawnChanceWeightAmmo(10);
            return true;
        }
        else
            return false;
    }

    public void AddMissile(){
        if(_currentlyMissile<3){
            _ammo[_currentlyMissile].gameObject.SetActive(true);
            _currentlyMissile++;
            // _powerUpWeightController.ChangeSpawnChanceWeightAmmo(-10);
        }
    }
}
