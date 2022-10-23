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
  }
}