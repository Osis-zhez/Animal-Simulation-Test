using CodeBase.Architecture.Services;
using UnityEngine;

namespace CodeBase.Architecture.AssetManagement
{
  public interface IAssetProvider : IService
  {
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path);
  }
}