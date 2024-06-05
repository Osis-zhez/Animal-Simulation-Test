using CodeBase.Data;

namespace CodeBase.Architecture.Services.PersistentProgress
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }
}