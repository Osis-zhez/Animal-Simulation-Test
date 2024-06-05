using System;
using System.Collections;
using CodeBase.Architecture.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Architecture
{
  public class SceneLoader
  {
    private GameStateMachine _stateMachine;
    private readonly ICoroutineRunner _coroutineRunner;

    
    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public void InjectStateMachine(GameStateMachine stateMachine) => 
      _stateMachine = stateMachine;

    public void Load(string name, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

    public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      // if (SceneManager.GetActiveScene().name == nextScene)
      // {
      //   onLoaded?.Invoke();
      //   yield break;
      // }
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      
      while (!waitNextScene.isDone)
        yield return null;

      Debug.Log("OnLoaded" + SceneManager.GetActiveScene().name);
      onLoaded?.Invoke();
    }

    public void RestartScene()
    {
      string currentScene = SceneManager.GetActiveScene().name;
      LoadScene("1. Empty");
      _stateMachine.Enter<LoadLevelState, string>(currentScene);
    }
  }
}