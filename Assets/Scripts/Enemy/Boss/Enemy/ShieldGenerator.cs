
using UnityEngine;

namespace Boss{
    public class ShieldGenerator : Enemy, IMoveable, ICanShoot
    {
        
        [SerializeField] private GameObject _beam;

        private float _angel;
        private Vector3 _targetPosition;
        private Transform _bossTransform;

        public override int GetSpawnChanceWeight(){return 0;}

        private void Awake() {
            _beam = gameObject.transform.GetChild(0).gameObject;
            _targetPosition = transform.position;
            _targetPosition.y  -=10;
        }

        public override void Start(){
            base.Start();
            _bossTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

            _angel = Vector3.SignedAngle(transform.position , _bossTransform.position - transform.position,transform.forward);

        }

        private void Update(){
            DoMove();
        }

        public void DoMove(){

            transform.position = Vector3.MoveTowards(transform.position,_targetPosition,_speed*Time.deltaTime);
            if(Vector3.Distance(transform.position,_targetPosition)< 0.1f)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,transform.rotation.y - _angel/1.5f),0.1f);
            if(transform.rotation ==  Quaternion.Euler(0,0,transform.rotation.y - _angel/1.5f))
                Shoot();

        }

        public void Shoot(){
            _beam.SetActive(true);
        }

        protected override void Dead(){
            GetComponentInParent<BossController>().ShieldGeneratorDead();
            base.Dead();
        }
    }
}