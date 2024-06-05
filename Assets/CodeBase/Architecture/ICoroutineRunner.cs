using System.Collections;
using UnityEngine;

namespace CodeBase.Architecture
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}