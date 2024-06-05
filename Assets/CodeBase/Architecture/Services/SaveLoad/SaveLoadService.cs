using CodeBase.Architecture.Factory;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Architecture.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService,
      IGameFactory gameFactory)
    {
      _progressService = progressService;
      _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress);
      
      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
      Debug.Log(PlayerPrefs.GetString(ProgressKey));
    }

    public PlayerProgress LoadProgress()
    {
      Debug.Log(PlayerPrefs.GetString(ProgressKey));
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }
}