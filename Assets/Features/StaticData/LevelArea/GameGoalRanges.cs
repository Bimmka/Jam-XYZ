using System;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct GameGoalRanges
  {
    public GoalPercent[] Percents;
    public float MinPercentToPassGame;
  }
}