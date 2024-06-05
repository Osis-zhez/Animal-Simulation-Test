using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class WorldData
  {
    public int MapSize;
    public int Animals;
    public int Velocity;
    public List<string> AnimalTypes;
    public Dictionary<string, Vector3Data> AnimalsSavePositions;

    public WorldData()
    {
      MapSize = 2;
      Animals = 1;
      Velocity = 1;
      
      AnimalTypes = new List<string>();
    }
  }
}