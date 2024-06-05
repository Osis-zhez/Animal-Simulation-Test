using System;

namespace CodeBase.Architecture.Services.SignalBus
{
  public class EventBusService : IService
  {
    public Action<int, int> OnGoldChanged;
  }
}