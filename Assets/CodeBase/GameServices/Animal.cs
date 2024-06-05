using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Data;
using UnityEngine;
using Random = System.Random;

namespace CodeBase.GameServices
{
   public class Animal : MonoBehaviour, ISavedProgress
   {
      public string Id;
 
      private IGameFactory _gameFactory;

      [SerializeField] private LayerMask _layerMask;
      [SerializeField] private Rigidbody _rb;

      private List<Vector3> _foodSpawnPoints = new List<Vector3>();
      private Transform _target;
      private Vector3 _moveDirection;
      public int _velocity = 5;
      private int _foodSpawnRadius = 5;
      private bool isStarted;

      public void Construct(IGameFactory gameFactory)
      {
         _gameFactory = gameFactory;
      }
      
      public void Move()
      {
         if (_target == null)
         {
            CreateFood();
         }

         Vector3 direction = _target.position - transform.position;
         direction.Normalize();
         _moveDirection = direction;
         float distance = Vector3.Distance(transform.position, _target.position);
         if (distance < 1.3f)
         {
            Destroy(_target.gameObject);
            CreateFood();
         }
         
         _rb.MovePosition(transform.position + _moveDirection * _velocity * Time.deltaTime);
      }

      public void CheckSpawnPoints()//Здесь происходит условное создание точек на расстоянии 5 сек вокруг юнита.
      {
         int radius = 3 * _velocity; // форму S = V × T здесь не дает нужного результата, видимо есть погрешность от юнити, поэтому для быстрого решения подобрал значение

         for(int i = 0 ; i < 360; i += 45)
         {
            float rad = (float)i / 180 * 3.14f;
            float x = transform.position.x + radius * Mathf.Cos(rad);
            float z = transform.position.z + radius * Mathf.Sin(rad);
            _foodSpawnPoints.Add(new Vector3( x, 0.5f, z));
         }
      }

      public void CreateFood() //Здесь точки проверяются, если есть коллизия с землей, тогда создается еда. Чтобы еда не создавал за землей
      {
         CheckSpawnPoints();
         
         for (int i = 0; i < 20; i++)
         {
            Collider[] _collisions = new Collider[1];
            int randomIndex = UnityEngine.Random.Range(0, _foodSpawnPoints.Count);
            Physics.OverlapSphereNonAlloc(_foodSpawnPoints[randomIndex], 1f, _collisions, _layerMask);

            if (_collisions[0] != null)
            {
               _target = _gameFactory.CreateFood(_foodSpawnPoints[randomIndex]);
               _foodSpawnPoints.Clear();
               break;
            }
         }
      }
      
      public string GenerateId() //Генерация Id, по имени сцены, чтобы закреплять юнитов за конкретной сценой
      {
         return Id = $"{gameObject.scene.name}_{Guid.NewGuid().ToString()}";
      }

      public void LoadProgress(PlayerProgress progress)
      {
         _velocity = progress.WorldData.Velocity;
      }

      public void UpdateProgress(PlayerProgress progress)
      {
         
      }
   }
}