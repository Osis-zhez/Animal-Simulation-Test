namespace CodeBase.Architecture.States
{
  public class GamePlayerTurnState : IState
  {
    private readonly GameStateMachine _stateMachine;

    public GamePlayerTurnState(GameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
    }
  }
}