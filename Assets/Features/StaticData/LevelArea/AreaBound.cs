using System;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct AreaBound
  {
    public Transform SpawnTransform;
    public LevelAreaType Area;
  }
}