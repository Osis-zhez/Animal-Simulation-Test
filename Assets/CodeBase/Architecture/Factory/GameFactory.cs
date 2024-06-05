using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using CodeBase.Architecture.AssetManagement;
using CodeBase.Architecture.Services;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Architecture.Services.Randomizer;
using CodeBase.Architecture.Services.SaveLoad;
using CodeBase.Architecture.Services.StaticData;
using CodeBase.Architecture.States;
using CodeBase.GameServices;
// using CodeBase.Logic.ResourcesLoot;
using CodeBase.StaticData;
using CodeBase.UI.Menu_UI;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Architecture.Factory
{
   public class GameFactory : IGameFactory
   {
      public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
      public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
      public List<IStartable> Starters { get; } = new List<IStartable>();

      public Dictionary<string, Animal> AnimalsDictionary { get; set; } = new Dictionary<string, Animal>();
      public List<Animal> AnimalList { get; set; } = new List<Animal>();

      private readonly IAssetProvider _assets;
      private readonly IStaticDataService _staticData;
      private readonly IRandomService _randomService;
      private readonly IPersistentProgressService _persistentProgressService;
      private readonly DIService _di;

      private float maxX;
      private float maxZ;

      public GameFactory(IAssetProvider assets, IStaticDataService staticData,
         IRandomService randomService, IPersistentProgressService persistentProgressService,
         DIService di)
      {
         _assets = assets;
         _staticData = staticData;
         _randomService = randomService;
         _persistentProgressService = persistentProgressService;
         _di = di;
      }

      public void WarmUp()
      {
         _di.WarmUp();
         AnimalsDictionary = new Dictionary<string, Animal>();
         AnimalList = new List<Animal>();
      }

      public void Cleanup()
      {
         ProgressReaders.Clear();
         ProgressWriters.Clear();
      }

      public async Task<UIRoot> CreateUIRoot()
      {
         GameObject prefab = Resources.Load<GameObject>("Game/UIRoot");
         GameObject gameObject = GameObject.Instantiate(prefab);
         RegisterProgressWatchers(gameObject);

         UIRoot uiRoot = gameObject.GetComponent<UIRoot>();
         _di.LocalDi.BindInstance<UIRoot>(uiRoot);
         _di.LocalDi.Inject(uiRoot);

         return uiRoot;
      }

      public async Task<MenuUIRoot> CreateMenuUiRoot(GameStateMachine stateMachine, ISaveLoadService saveLoadService)
      {
         GameObject prefab = Resources.Load<GameObject>("Game/Menu_UI_Root");
         GameObject gameObject = GameObject.Instantiate(prefab);
         RegisterProgressWatchers(gameObject);

         MenuUIRoot ui = gameObject.GetComponent<MenuUIRoot>();
         ui.Construct(stateMachine, saveLoadService);

         return ui;
      }

      public async Task CreatePlane(float size)
      {
         GameObject prefab = Resources.Load<GameObject>("Game/Plane");
         GameObject gameObject = GameObject.Instantiate(prefab);
         
         gameObject.transform.localScale = new Vector3(size, 1, size);
         maxX = size * 10;
         maxZ = size * 10;
      }

      public async Task<Animal> CreateAnimal()
      {
         float randomX = Random.Range(-maxX / 2, maxX / 2);
         float randomZ = Random.Range(- maxZ / 2, maxZ / 2);
         GameObject prefab = Resources.Load<GameObject>("Game/Animal");
         GameObject gameObject = GameObject.Instantiate(prefab, new Vector3(randomX, 0.5f, randomZ), Quaternion.identity);
         RegisterProgressWatchers(gameObject);

         Animal animal = gameObject.GetComponent<Animal>();
         animal.Construct(this);
         AnimalsDictionary.Add(animal.GenerateId(), animal);
         // AnimalList.Add(animal);

         return animal;
      }
      
      public async Task<Animal> LoadAnimal(string id)
      {
         float randomX = Random.Range(-maxX / 2, maxX / 2);
         float randomZ = Random.Range(- maxZ / 2, maxZ / 2);
         GameObject prefab = Resources.Load<GameObject>("Game/Animal");
         GameObject gameObject = GameObject.Instantiate(prefab, new Vector3(randomX, 0.5f, randomZ), Quaternion.identity);
         RegisterProgressWatchers(gameObject);

         Animal animal = gameObject.GetComponent<Animal>();
         animal.Construct(this);
         animal.Id = id;
         
         return animal;
      }

      public Transform CreateFood(Vector3 position)
      {
         GameObject prefab = Resources.Load<GameObject>("Game/Food");
         GameObject gameObject = GameObject.Instantiate(prefab, position, Quaternion.identity);

         return gameObject.transform;
      }

      public void CreateAnimalSystem()
      {
         GameObject prefab = Resources.Load<GameObject>("Game/AnimalsSystem");
         GameObject gameObject = GameObject.Instantiate(prefab);
         RegisterProgressWatchers(gameObject);
         
         gameObject.GetComponent<AnimalsSystem>().Construct(this);
      }

      private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
      {
         GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
         RegisterProgressWatchers(gameObject);
         RegisterStarters(gameObject);

         return gameObject;
      }

      private GameObject InstantiateRegistered(string prefabPath)
      {
         GameObject gameObject = _assets.Instantiate(path: prefabPath);
         RegisterProgressWatchers(gameObject);

         return gameObject;
      }

      private void RegisterProgressWatchers(GameObject gameObject)
      {
         foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
      }

      private void RegisterStarters(GameObject gameObject)
      {
         foreach (IStartable subcriber in gameObject.GetComponentsInChildren<IStartable>())
            Starters.Add(subcriber);
      }

      private void Register(ISavedProgressReader progressReader)
      {
         if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);

         ProgressReaders.Add(progressReader);
      }
   }
}