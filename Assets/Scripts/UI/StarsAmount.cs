using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsAmount : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private Sprite _star;

    public static int _starsAmount;

    private void Awake() {
        for(int i = 0; i < transform.childCount; i++){
            _stars[i] = transform.GetChild(i).GetChild(0).gameObject;
        }
    }

    private void Start() {
        
    }

}
