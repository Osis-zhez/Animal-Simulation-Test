using System.Collections.Generic;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Architecture.Services.ResourcesLoot
{
  public class ResourcesService : MonoBehaviour, IResourcesService, ISavedProgress
  {
    public List<IResource> ResourcesList { get; set; } = new List<IResource>();
    
    public GoldResource GoldResource { get; set; }
    public SparePartsResource SparePartsResource { get; set; }

    public void Construct()
    {
      ResourcesServiceInit();
    }

    public void ResourcesServiceInit()
    {
      GoldResource = new GoldResource();
      SparePartsResource = new SparePartsResource();
      
      ResourcesList = new List<IResource>()
      {
        GoldResource, SparePartsResource
      };
    }
    
    public void IncreaseResource(ResourcesId resourceID, int amount)
    {
      foreach (IResource resource in ResourcesList)
        if (resource.ResourcesID == resourceID)
          resource.Increase(amount);
    }

    public void DecreaseResource(ResourcesId resourceID, int amount)
    {
      foreach (IResource resource in ResourcesList)
        if (resource.ResourcesID == resourceID)
          resource.Decrease(amount);
    }

    public int GetResourceAmount(ResourcesId resourcesID)
    {
      foreach (IResource resource in ResourcesList)
        if (resource.ResourcesID == resourcesID)
          return resource.Amount;
      return 0;
    }

    public void SetResourceAmount(ResourcesId resourcesID, int amount)
    {
      foreach (IResource resource in ResourcesList)
        if (resource.ResourcesID == resourcesID)
          resource.Amount = amount;
    }

    public IResource GetResourceClass(ResourcesId resourcesID)
    {
      foreach (IResource resource in ResourcesList)
        if (resourcesID == resource.ResourcesID)
          return resource;
      return null;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      // SetResourceAmount(ResourcesId.Gold, progress.LootData.GoldAmount);
      // SetResourceAmount(ResourcesId.SpareParts, progress.LootData.SparePartsAmount);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      // progress.LootData.GoldAmount = GetResourceAmount(ResourcesId.Gold);
      // progress.LootData.SparePartsAmount = GetResourceAmount(ResourcesId.SpareParts);
    }

    public void OnAfterDeserialize()
    {
      
    }
  }
}