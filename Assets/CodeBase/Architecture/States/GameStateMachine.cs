using System;
using System.Collections.Generic;
using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services;
using CodeBase.Architecture.Services.Input;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.Services.StaticData;

namespace CodeBase.Architecture.States
{
  public class GameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, DIService di)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, di),
        [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, loadingCurtain, 
          di.Container.Resolve<IGameFactory>(), di.Container.Resolve<IPersistentProgressService>(), 
          di.Container.Resolve<IStaticDataService>(), di.Container.Resolve<InputService>(),
          di.Container.Resolve<ISaveLoadService>()),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, 
          di.Container.Resolve<IGameFactory>(), di.Container.Resolve<IPersistentProgressService>(), 
          di.Container.Resolve<IStaticDataService>(),
          di.Container.Resolve<InputService>()),
        [typeof(LoadProgressState)] = new LoadProgressState(this, 
          di.Container.Resolve<IPersistentProgressService>(), 
          di.Container.Resolve<ISaveLoadService>(), di.Container.Resolve<IStaticDataService>()),
        [typeof(GamePlayState)] = new GamePlayState(this),
        [typeof(MetaGameState)] = new MetaGameState(this),
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}