using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services.Input;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.Services.StaticData;
using CodeBase.UI.Menu_UI;
using UnityEngine;

namespace CodeBase.Architecture.States
{
   public class MainMenuState : IState
   {
      private readonly GameStateMachine _stateMachine;
      private readonly SceneLoader _sceneLoader;
      private readonly LoadingCurtain _curtain;
      private readonly IGameFactory _gameFactory;
      private readonly IPersistentProgressService _progressService;
      private readonly IStaticDataService _staticData;
      private readonly InputService _inputService;
      private readonly ISaveLoadService _saveLoadService;

      public MainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, 
         LoadingCurtain curtain, IGameFactory gameFactory, 
         IPersistentProgressService progressService, IStaticDataService staticData, 
         InputService inputService, ISaveLoadService saveLoadService)
      {
         _stateMachine = stateMachine;
         _sceneLoader = sceneLoader;
         _curtain = curtain;
         _gameFactory = gameFactory;
         _progressService = progressService;
         _staticData = staticData;
         _inputService = inputService;
         _saveLoadService = saveLoadService;
      }

      public void Enter()
      {
         _curtain.Show();
         
         _sceneLoader.Load("1. MainMenu", OnLoaded);
      }

      public void Exit()
      {
         
      }

      private void OnLoaded()
      {
         InitUI();
         _curtain.Hide();
      }

      private async void InitUI()
      {
         await _gameFactory.CreateMenuUiRoot(_stateMachine, _saveLoadService);
      }
   }
}