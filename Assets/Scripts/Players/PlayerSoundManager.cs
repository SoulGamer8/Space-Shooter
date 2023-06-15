using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundManager : MonoBehaviour
{
 
    private Shoot _shoot;
    private PlayerHealth _playerHealth;


    private AudioSource _audioSource;

    private void OnEnable()
    {
        _shoot.PlaySoundSoot += PlaySound;
        _playerHealth.PlayerDieSound += PlaySound;
    }

    private void OnDisable()
    {
        _shoot.PlaySoundSoot -= PlaySound;
        _playerHealth.PlayerDieSound -= PlaySound;
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _playerHealth = GetComponent<PlayerHealth>();
        _shoot = GetComponent<Shoot>();
    }

    
    private void PlaySound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
