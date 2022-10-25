using System;
using Features.Police.Data;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct PoliceLevelData
  {
    public PoliceType Type;
    public LevelAreaType Area;
    public Vector3 StartPosition;
    public Vector3[] PatrolPath;
    public Vector3[] SearchPath;

    public PoliceLevelData(PoliceType type, LevelAreaType area, Vector3 startPosition, Vector3[] patrolPath, Vector3[] searchPath)
    {
      Type = type;
      Area = area;
      StartPosition = startPosition;
      PatrolPath = patrolPath;
      SearchPath = searchPath;
    }
  }
}