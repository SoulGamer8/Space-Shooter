using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _takeCoinSound;


    private AudioSource _audioSorce;
    private WalletManager _walletManager;

    private void Awake() {
        _audioSorce = GetComponent<AudioSource>();
        _walletManager = WalletManager.instance;
    }   


    private void Update() {
        transform.Translate(Vector3.down *_speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player")
        {
             Debug.Log("Test");
            _audioSorce.Play();
            _walletManager.SetMoney(1);
            Destroy(gameObject,0.4f);

        }
          
    }

}
