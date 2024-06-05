using System;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.States;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu_UI
{
   public class MenuUIRoot : MonoBehaviour
   {
      [SerializeField] private Button _startButton;
      private GameStateMachine _stateMachine;
      private ISaveLoadService _saveLoadService;

      public void Construct(GameStateMachine stateMachine, ISaveLoadService saveLoadService)
      {
         _stateMachine = stateMachine;
         _saveLoadService = saveLoadService;
         _startButton.onClick.AddListener(StartGame);
      }

      private void StartGame()
      {
         _saveLoadService.SaveProgress();
         _stateMachine.Enter<LoadLevelState, string>("2. GamePlay");
      }
      
   }
}