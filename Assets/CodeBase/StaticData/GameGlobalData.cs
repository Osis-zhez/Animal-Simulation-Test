using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "GameGlobalData", menuName = "Static Data/GameGlobal")]
  public class GameGlobalData : ScriptableObject, ISerializationCallbackReceiver
  {
    [Header("New Game Start Settings")] public int HealthPacks;
    public int Grenades;
    public int RifleAmmo;
    public int ShotgunAmmo;
    
    public void OnBeforeSerialize()
    {
      
    }

    public void OnAfterDeserialize()
    {
      
    }
  }
  
}