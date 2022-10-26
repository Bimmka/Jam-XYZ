using System;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct GoalPercent
  {
    public int MinPointCount;
    public int MaxPointCount;
    public float PassPercent;
  }
}