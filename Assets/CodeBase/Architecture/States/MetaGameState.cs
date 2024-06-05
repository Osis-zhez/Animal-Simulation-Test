namespace CodeBase.Architecture.States
{
  public class MetaGameState : IState
  {
    private readonly GameStateMachine _stateMachine;

    public MetaGameState(GameStateMachine stateMachine)
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