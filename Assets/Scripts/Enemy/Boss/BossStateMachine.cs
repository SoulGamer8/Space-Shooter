
public class BossStateMachine 
{
    public BossState currentEnemyState;

    public void Initialize(BossState startingState){
        currentEnemyState = startingState;
        currentEnemyState.OnEnter();
    }

    public void ChangeState(BossState newState){
        currentEnemyState.OnExit();
        currentEnemyState = newState;
        currentEnemyState.OnEnter();
    }

}
