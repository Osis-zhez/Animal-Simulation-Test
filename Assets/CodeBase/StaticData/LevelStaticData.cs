using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public string WinScreenLevelKey;

    
    // public Dictionary<LocalServiceId, type>
    // public List<LocalServiceId> LocalServices;
    // public List<EnemySpawnerStaticData> EnemySpawners;
  }
}