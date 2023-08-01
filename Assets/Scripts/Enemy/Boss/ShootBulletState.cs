using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootBulletState : State
{
      private GameObject _bullet;
      private float _fireRateBullet;

      private float _fireRate = 2f;
     
      private float timer = 0.0f;

      public override void OnEnter(GameObject gameObject,float fireRate){

      }

      public override void OnExit()
      {
          Debug.Log("OnExit");
      }

      public override void UpdateState()
      {

          timer += Time.deltaTime;

          if (timer > _fireRate)
          {
            Debug.Log("UpdateState");


            timer = timer - _fireRate;
          }
      }

}
