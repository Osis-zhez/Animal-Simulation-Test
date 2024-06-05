﻿using CodeBase.Architecture.States;
using UnityEngine;

namespace CodeBase.Architecture
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public LoadingCurtain CurtainPrefab;
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab));
      _game.StateMachine.Enter<BootstrapState>();
      
        DontDestroyOnLoad(this);   
    }
  }
}