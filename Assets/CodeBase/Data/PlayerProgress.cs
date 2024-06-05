using System;
using CodeBase.Architecture.Services.StaticData;
using CodeBase.Data.ResourcesLootData;
using CodeBase.StaticData;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    
    public WorldData WorldData;

    public PlayerProgress(IStaticDataService staticDataService, string initialLevel)
    {
      WorldData = new WorldData();
    }
  }
}