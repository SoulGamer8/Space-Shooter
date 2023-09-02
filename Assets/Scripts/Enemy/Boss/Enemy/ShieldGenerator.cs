
using UnityEngine;

namespace Boss{
    public class ShieldGenerator : Enemy
    {
        
        [SerializeField] private GameObject _beam;

        private float _angel;
        private Vector3 _targetPosotion;
        private Transform _bossTransform;

        public override int GetSpawnChanceWeight(){return 0;}

        private void Awake() {
            _beam = gameObject.transform.GetChild(0).gameObject;
            _targetPosotion = transform.position;
            _targetPosotion.y  -=10;
        }

        private void Start(){
            _bossTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

            _angel = Vector3.SignedAngle(transform.position , _bossTransform.position - transform.position,transform.forward);

        }

        protected override void OnTriggerEnter2D(Collider2D collider){
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if(collider.tag == "Player")
                damageable.Damege(1);
        }

        private void Update(){
            DoMove();
        }

        public override void DoMove(){

            transform.position = Vector3.MoveTowards(transform.position,_targetPosotion,_speed*Time.deltaTime);
            if(Vector3.Distance(transform.position,_targetPosotion)< 0.1f){

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,transform.rotation.y - _angel/1.5f),0.1f);

                // transform.LookAt(_bossTransform.position-transform.position,Vector3.forward);


                // var targetRotation = Quaternion.LookRotation(_bossTransform.position - transform.position);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
            if(transform.rotation ==  Quaternion.Euler(0,0,_angel))
                Shoot();

        }

        public override void Shoot(){
            _beam.SetActive(true);
        }

        public override void Damege(int damage){
            _health -= damage;
            if(_health < 0)
                Dead();
        }

        protected override void Dead(){
            GetComponentInParent<BossController>().ShieldGeneratorDead();
            Destroy(this.gameObject);
        }
    }
    }