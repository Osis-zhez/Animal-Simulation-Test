namespace CodeBase.Architecture.States
{
   public class GamePlayState : IState
   {
      private GameStateMachine _stateMachine;

      public GamePlayState(GameStateMachine stateMachine)
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