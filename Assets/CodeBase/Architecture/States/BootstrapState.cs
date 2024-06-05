using System.Collections.Generic;
using CodeBase.Architecture.AssetManagement;
using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services;
using CodeBase.Architecture.Services.Input;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.Randomizer;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.Services.StaticData;

namespace CodeBase.Architecture.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "0. Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly DIService _di;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, DIService di)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _di = di;

      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void RegisterServices()
    {

      _di.Container.BindInstance<IStaticDataService>(new StaticDataService());
      _di.Container.BindInstance<GameStateMachine>(_stateMachine);
      _di.Container.BindInstance<InputService>(new InputService()).AsSingle();
      _di.Container.BindInstance<IPersistentProgressService>(new PersistentProgressService()).AsSingle();
      _di.Container.BindInstance<IRandomService>(new RandomService()).AsSingle();
      _di.Container.BindInstance<IAssetProvider>(new AssetProvider()).AsSingle();

      _di.Container.BindInstance<IGameFactory>(new GameFactory(
        _di.Container.Resolve<IAssetProvider>(),
        _di.Container.Resolve<IStaticDataService>(),
        _di.Container.Resolve<IRandomService>(),
        _di.Container.Resolve<IPersistentProgressService>(),
        DIService.Instance)).AsSingle();
      _di.Container.BindInstance<ISaveLoadService>(new SaveLoadService(
        _di.Container.Resolve<IPersistentProgressService>(),
        _di.Container.Resolve<IGameFactory>())).AsSingle();
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadProgressState>();

    // private static IInputService InputService() =>
    //   Application.isEditor
    //     ? (IInputService) new StandaloneInputService()
    //     : new MobileInputService();
  }
}