using System.Collections.Generic;
using UnityEngine;

public class AmmoMissile : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> _ammo;

    private int _curentlyMissile;
    private void Awake() {
        _curentlyMissile = this.gameObject.transform.childCount;
        for(int i = 0;i<this.gameObject.transform.childCount;i++){
            _ammo.Add(this.gameObject.transform.GetChild(i).gameObject);
        }
    }

    public bool UseMissile(){
        if(_curentlyMissile>0){
            _ammo[_curentlyMissile-1].gameObject.SetActive(false);
            _curentlyMissile--;
            return true;
        }
        else
            return false;
    }

    public void AddMissile(){
        if(_curentlyMissile<3){
            _ammo[_curentlyMissile].gameObject.SetActive(true);
            _curentlyMissile++;
        }
    }
}
