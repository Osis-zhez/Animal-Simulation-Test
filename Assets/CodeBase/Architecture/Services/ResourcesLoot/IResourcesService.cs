using System.Collections.Generic;

namespace CodeBase.Architecture.Services.ResourcesLoot
{
  public interface IResourcesService : IService
  {
    public List<IResource> ResourcesList { get; set; }
    GoldResource GoldResource { get; set; }
    SparePartsResource SparePartsResource { get; set; }
    int GetResourceAmount(ResourcesId resourcesID);
    IResource GetResourceClass(ResourcesId resourcesID);
    void IncreaseResource(ResourcesId resourceID, int amount);
    public void DecreaseResource(ResourcesId resourceID, int amount);
    
  }
}