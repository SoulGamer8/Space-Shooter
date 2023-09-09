 using System.Collections.Generic;
using UnityEngine;

public class SpeedShipController : MonoBehaviour, ISpawnChanceWeight, IScore
{
    [SerializeField] private GameObject _speedShip;
    [SerializeField] private List<GameObject> _speedShipList;

    [SerializeField] private int _maxAmountShip;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private bool _isNegative = false;
    
    [SerializeField] private int _spawnChanceWeight;

    private int _speedShipAlive;

    private int _sumScore;

    private float _positionY;

    private void Awake() {
        for(int i = 0;i<_maxAmountShip;i++){
            _sumScore +=_speedShip.GetComponent<IScore>().GetScore();
        }
    }

    private void Start() {
        _positionY = transform.position.y;
       

        Spawn();
    }

    private void Spawn(){
        GameObject ship;
        for(int i=0;i<_maxAmountShip;i++){
            ship = Instantiate(_speedShip,new Vector3(transform.position.x,_positionY),Quaternion.identity,transform);
            _sumScore +=ship.GetComponent<IScore>().GetScore();
            SetSettingShip(ship);
            _speedShipList.Add(ship);
            _positionY +=1.5f;
        }
    }

    private void SetSettingShip(GameObject ship){
        ship.GetComponent<SpeedEnemy>().SetSin(_amplitude,_frequency,_isNegative);
    }

    public void SpeedShipDead(){
        _speedShipAlive--;
        if(_speedShipAlive >=0)
            Destroy(gameObject);
    }

    public int GetSpawnChanceWeight(){
        return _spawnChanceWeight;
    }

    public int GetScore(){
        return _sumScore;
    }
}
