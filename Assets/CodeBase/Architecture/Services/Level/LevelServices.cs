using CodeBase.Architecture.Services.ResourcesLoot;
using CodeBase.Architecture.Services.StaticData;
using UnityEngine;

namespace CodeBase.Architecture.Services.Level
{
  public class LevelServices : MonoBehaviour
  {
    [SerializeField] public ResourcesService ResourcesService;
 

    public void Construct(IStaticDataService staticDataService)
    {
     
    }
  }
}

// public Dictionary<Type, MonoBehaviour> Services = new Dictionary<Type, MonoBehaviour>();
// foreach (MonoBehaviour service in gameObject.GetComponentsInChildren<MonoBehaviour>())
// {
// Services.Add(service.GetType(), service);
// }