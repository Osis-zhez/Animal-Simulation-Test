using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Architecture.Services;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.States;
using CodeBase.GameServices;
using CodeBase.UI.Menu_UI;
using UnityEngine;

namespace CodeBase.Architecture.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    List<IStartable> Starters { get; }
    Dictionary<string, Animal> AnimalsDictionary { get; }
    List<Animal> AnimalList { get; }

    void Cleanup();
    Task<Animal> CreateAnimal();
    Task CreatePlane(float size);
    void WarmUp();
    Task<MenuUIRoot> CreateMenuUiRoot(GameStateMachine stateMachine, ISaveLoadService saveLoadService);
    // Transform CreateFood(Vector3 position);
    void CreateAnimalSystem();
    Transform CreateFood(Vector3 position);
    Task<UIRoot> CreateUIRoot();
  }
}