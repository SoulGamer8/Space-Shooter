using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void OnEnter(GameObject gameObject,float fireRate);
    public abstract void UpdateState();
    public abstract void OnExit();

}


