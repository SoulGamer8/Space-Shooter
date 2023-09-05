using UnityEngine;

namespace Boss{
    public class BossAudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip){
            _audioSource.PlayOneShot(clip);
        }


    }
}