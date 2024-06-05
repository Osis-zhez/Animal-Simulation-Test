using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Architecture.Services.Audio
{
  public class AudioService : MonoBehaviour
  {
    public AudioSource _musicASource;
    public AudioSource _soundASource;
    public AudioSource _stepASource;
    public Dictionary<AudioId, AudioClip> _soundDictionary = new Dictionary<AudioId, AudioClip>();

    private void Awake()
    {
      DontDestroyOnLoad(gameObject);

      foreach (AudioId audio in System.Enum.GetValues(typeof(AudioId)))
      {
        _soundDictionary[audio] = Resources.Load<AudioClip>("Audio/" + audio.ToString());
      }
      
      PlayMusic(AudioId.Level1Music);
    }

    public void PlaySfx(AudioId audioId)
    {
      _soundASource.PlayOneShot(_soundDictionary[audioId]);
    }

    public void PlayMusic(AudioId audioId)
    {
      _musicASource.clip = _soundDictionary[audioId];
      _musicASource.Play();
    }

    public void PlaySfxAdjusted(AudioId audioId)
    {
      _soundASource.pitch = Random.Range(0.9f, 1.1f);
      _soundASource.PlayOneShot(_soundDictionary[audioId]);
    }

    public void PlayStepSfx(AudioId audioId)
    {
      _stepASource.pitch = Random.Range(0.9f, 1.1f);
      _stepASource.PlayOneShot(_soundDictionary[audioId]);
    }
  }
}