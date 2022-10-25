using System;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct AreaBound
  {
    public Vector3 SpawnPosition;
    public LevelAreaType Area;

    public AreaBound(Vector3 spawnPosition, LevelAreaType area)
    {
      SpawnPosition = spawnPosition;
      Area = area;
    }
  }
}