using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.GameServices
{
   public class UIRoot : MonoBehaviour
   {
      [SerializeField] private Button _saveQuitBtn;
      private GameStateMachine _stateMachine;
      private ISaveLoadService _saveLoadService;

      [Inject]
      public void Construct(GameStateMachine stateMachine, ISaveLoadService saveLoadService)
      {
         _stateMachine = stateMachine;
         _saveLoadService = saveLoadService;
         
         _saveQuitBtn.onClick.AddListener(SaveQuit);
      }


      private void SaveQuit()
      {
         _saveLoadService.SaveProgress();
         _stateMachine.Enter<MainMenuState>();
      }
   }
}