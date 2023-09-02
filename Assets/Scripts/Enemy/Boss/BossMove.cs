using UnityEngine;

namespace Boss{
    public class BossMove : MonoBehaviour
    {
        [SerializeField] private float _speed = 0;
        [SerializeField] private Vector3 _patrulingArea;

        public void SetSpeed(float speed) => _speed = speed > 0 ? speed : 0; 

        void Update(){
            DoMove();
        }

        private void DoMove(){
            transform.position = Vector3.MoveTowards(transform.position, _patrulingArea, _speed * Time.deltaTime);
                if(Vector3.Distance(transform.position, _patrulingArea) < 0.5f)
                    _patrulingArea.x *=-1;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_patrulingArea, new Vector3(_patrulingArea.x*-1,_patrulingArea.y,_patrulingArea.z));
        }
    }
}