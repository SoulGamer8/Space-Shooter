public abstract class BossState{

    protected BossController bossController;
    protected BossStateMachine bossStateMachine;

    public BossState(BossController bossController, BossStateMachine bossStateMachine){
        this.bossController = bossController;
        this.bossStateMachine = bossStateMachine;
    }

    public abstract void OnEnter();
    public abstract void UpdateState();
    public abstract void OnExit();

}



