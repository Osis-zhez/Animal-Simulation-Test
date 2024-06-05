namespace CodeBase.Architecture.States
{
  public class GameEnemyTurnState : IState
  {
    private readonly GameStateMachine _stateMachine;

    public GameEnemyTurnState(GameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }

    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }
  }
}