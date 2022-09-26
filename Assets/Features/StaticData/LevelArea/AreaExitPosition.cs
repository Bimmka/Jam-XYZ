using System;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct AreaExitPosition
  {
    public LevelAreaType Area;
    public Transform[] Positions;
  }
}