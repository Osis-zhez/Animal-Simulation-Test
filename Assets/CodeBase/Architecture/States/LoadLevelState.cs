using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services.Input;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.StaticData;
using CodeBase.Data;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using IInitializable = CodeBase.Architecture.Factory.IInitializable;
using LevelServices = CodeBase.GameServices.LevelServices;

namespace CodeBase.Architecture.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly InputService _inputService;

    private LevelServices _levelServices;
    private DiContainer _levelDi;
    private SceneContext _sceneContext;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain, IGameFactory gameFactory,
      IPersistentProgressService progressService, IStaticDataService staticDataService,
      InputService inputService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticDataService;
      _inputService = inputService;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _gameFactory.Cleanup();
      _gameFactory.WarmUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _loadingCurtain.Hide();
    }

    private async void OnLoaded()
    {
      await InitGameWorld();
      await InitUI();
      await InitPresenters();
         
      InformProgressReaders();
         
      _stateMachine.Enter<GamePlayState>();
    }

    private async Task InitGameWorld()
    {
      await _gameFactory.CreatePlane(_progressService.Progress.WorldData.MapSize);
      await CreateAnimals();
      _gameFactory.CreateAnimalSystem();
    }

    private async Task CreateAnimals()
    {
      for (int i = 0; i < _progressService.Progress.WorldData.Animals; i++)
      {
        await _gameFactory.CreateAnimal();
      }
    }

    private async Task InitUI()
    {
      await _gameFactory.CreateUIRoot();
    }

    private async Task InitPresenters()
    {
      
    }
    
    private void InformProgressReaders()
    {
      Debug.Log(PlayerPrefs.GetString("Progress"));
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }
  }
}