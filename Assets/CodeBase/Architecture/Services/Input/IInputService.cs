using UnityEngine;

namespace CodeBase.Architecture.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }

    bool IsAttackButtonUp();
  }
}