using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.Services.StaticData;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Architecture.States
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadProgress;
    private readonly IStaticDataService _staticDataService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, 
      ISaveLoadService saveLoadProgress, IStaticDataService staticDataService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadProgress = saveLoadProgress;
      _staticDataService = staticDataService;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();
      
      _gameStateMachine.Enter<MainMenuState>();
      // _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
      
      _progressService.Progress = 
        _saveLoadProgress.LoadProgress() 
        ?? NewProgress();
      Debug.Log(PlayerPrefs.GetString("Progress"));
      foreach (string value in _progressService.Progress.WorldData.AnimalTypes)
      {
        Debug.Log(value);
      }
    }

    private PlayerProgress NewProgress()
    {
      var progress =  new PlayerProgress(_staticDataService, initialLevel: "1. MainMenu");

      return progress;
    }
  }
}