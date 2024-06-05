using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Architecture
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;
    public static Action OnHide;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => 
      StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      Debug.Log("Curtain");
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      OnHide?.Invoke();
      gameObject.SetActive(false);
    }
  }
}