using UnityEngine;

namespace SpaceShooter
{
    public class Explosion : MonoBehaviour
    {
        private Animator _animator;

        void Start(){
            _animator = GetComponent<Animator>();
        
            Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
