using UnityEngine;
using Zenject;

namespace CodeBase.Architecture.Services.Level
{
  public class LocalDiContainer : MonoInstaller, ISerializationCallbackReceiver
  {
    public static LocalDiContainer Instance { get; private set; }

    public override void InstallBindings()
    {
      Debug.Log("SceneContext");
      
    }

    public DiContainer GetContainer() => 
      Container;

    public void OnAfterDeserialize() => 
      Instance = this;

    public void OnBeforeSerialize()
    {
    }
  }
}