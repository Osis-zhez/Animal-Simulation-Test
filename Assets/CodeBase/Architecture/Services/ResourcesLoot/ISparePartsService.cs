using System;

namespace CodeBase.Architecture.Services.ResourcesLoot
{
  public interface ISparePartsService : IService
  {
    event Action<int> OnAmountChanged;
    int Amount { get; set; }
    void Increase(int amount);
    void Deacrease(int amount);
    
  }
}