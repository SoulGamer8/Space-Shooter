using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLaserState : BossState
{
    private float _timer;

    private Transform transform;
    private Vector3 _patruling;
    private float _speed;
    public ShootingLaserState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){

    }

    public override void OnEnter(){

        _speed = bossController._bossSpeed;

        transform = bossController.transform;
        _patruling = new Vector3(6,transform.position.y);
    }

    public override void OnExit(){
        
    }

    public override void UpdateState(){
        if(_timer > bossController._fireRateBullet){
            int angelBetweenLaser = 2 * bossController._volleyLaserSpread / (bossController._countLaserToFire-1);

            for(int angle = -bossController._volleyLaserSpread;angle<= bossController._volleyLaserSpread;angle += angelBetweenLaser){
                Debug.Log(angle);
                GameObject bullet = GameObject.Instantiate(bossController._bullet,bossController.transform.position,Quaternion.Euler(0,0,angle));  
            }

            _timer = 0;
        }

        _timer +=Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _patruling, _speed *Time.deltaTime);
        if(Vector3.Distance(transform.position, _patruling) < 0.5f)
            _patruling.x *=-1;
    }
}
