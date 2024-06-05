using System.Collections;
using System.Collections.Generic;
using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.GameServices
{
   public class AnimalsSystem : MonoBehaviour, ISavedProgress
   {
      private IGameFactory _gameFactory;
      private Dictionary<string, Animal> _animalsDictionary;
      private bool isStarted;

      public void Construct(IGameFactory gameFactory)
      {
         _gameFactory = gameFactory;
         _animalsDictionary = _gameFactory.AnimalsDictionary;
      }

      private void Start()
      {
         StartCoroutine(Wait());
      }

      private void FixedUpdate()
      {
         if (!isStarted) return;
      
         foreach (KeyValuePair<string,Animal> keyValuePair in _animalsDictionary)
         {
            keyValuePair.Value.Move();
         }
      }

      IEnumerator Wait()
      {
         yield return new WaitForSeconds(0.2f);
         isStarted = true;
      }

      public void LoadProgress(PlayerProgress progress)
      {
      
      }

      public void UpdateProgress(PlayerProgress progress)
      {
         foreach (KeyValuePair<string,Animal> keyValuePair in _animalsDictionary) //записываем ключи
            progress.WorldData.AnimalTypes.Add(keyValuePair.Key);

         // foreach (KeyValuePair<string,Animal> keyValuePair in _animalsDictionary) //записываем вектор3 под ключи, чтобы загрузить игру
         // {
         //    progress.WorldData.AnimalsSavePositions = new Dictionary<string, Vector3Data>();
         //    progress.WorldData.AnimalsSavePositions.Add(keyValuePair.Key, keyValuePair.Value.transform.position.AsVectorData());
         // }
      }
   }
}