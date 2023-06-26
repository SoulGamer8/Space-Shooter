using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   private PlayerController _input;

    [SerializeField] private GameManager _gameManager;
    private void Awake()
    {
        _input = new PlayerController();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
