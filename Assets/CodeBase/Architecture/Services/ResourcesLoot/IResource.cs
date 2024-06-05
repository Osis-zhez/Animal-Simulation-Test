using System;

namespace CodeBase.Architecture.Services.ResourcesLoot
{
  public interface IResource
  {
    ResourcesId ResourcesID { get; set; }
    event Action<int, int> OnIncrease;
    int Amount { get; set; }
    void Increase(int addAmount);
    void Decrease(int addAmount);
  }
}