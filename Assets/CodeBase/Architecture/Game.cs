using CodeBase.Architecture.Services;
using CodeBase.Architecture.States;

namespace CodeBase.Architecture
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, DIService.Instance);
    }
  }
}