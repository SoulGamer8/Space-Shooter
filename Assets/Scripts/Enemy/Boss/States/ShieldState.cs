using UnityEngine;

public class ShieldState : BossState
{
    private int _curentlyShieldGenerator = 2;
    public ShieldState(BossController bossController, BossStateMachine bossStateMachine) : base(bossController, bossStateMachine){}

    public override void OnEnter(){
        bossController._shield.SetActive(true);

        GameObject.Instantiate(bossController._shieldGenerator,new Vector3(8,10,0),Quaternion.Euler(0,0,0),bossController.transform);
        GameObject.Instantiate(bossController._shieldGenerator,new Vector3(-8,10,0),Quaternion.Euler(0,0,0),bossController.transform);

    }

    public void ShieldGeneratorDestroy(){
        _curentlyShieldGenerator--;
        WeekShield();
        
        if(_curentlyShieldGenerator<=0)
           bossStateMachine.ChangeState(bossController.beamState);
    }

    private void WeekShield(){
        Color tmp = bossController._shield.GetComponent<SpriteRenderer>().color;
        tmp.a = bossController._weekShieldOpacity;
        bossController._shield.GetComponent<SpriteRenderer>().color = tmp;
    }

    public override void UpdateState(){
        bossController.transform.position = Vector3.MoveTowards(bossController.transform.position,bossController._target,0.1f);
    }

    public override void OnExit(){
        bossController._shield.SetActive(false);
        bossController._isShieldActive = false;
    }
}
