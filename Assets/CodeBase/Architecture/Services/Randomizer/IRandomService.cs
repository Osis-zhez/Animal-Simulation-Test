namespace CodeBase.Architecture.Services.Randomizer
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
  }
}