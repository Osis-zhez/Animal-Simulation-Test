using UnityEngine;

namespace CodeBase.GameServices
{
   public class Food : MonoBehaviour
   {
      public ParticleSystem _vfx;

      public void Pickup()
      {
         // _vfx.Play();
         Destroy(gameObject);
      }
   }
}