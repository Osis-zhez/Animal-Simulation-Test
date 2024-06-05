using System;

namespace CodeBase.Architecture.Services.ResourcesLoot
{
  public class GoldResource : IResource
  {
    public event Action<int, int> OnIncrease;
    public ResourcesId ResourcesID { get; set; } = ResourcesId.Gold;

    public int Amount { get; set; }

    public GoldResource()
    {
    }

    public void Increase(int addAmount)
    {
      Amount += addAmount;
      OnIncrease?.Invoke(Amount, addAmount);
    }

    public void Decrease(int addAmount)
    {
      Amount -= addAmount;
    }
  }
}