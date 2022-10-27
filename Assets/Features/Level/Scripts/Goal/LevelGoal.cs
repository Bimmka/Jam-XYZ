using Features.StaticData.LevelArea;

namespace Features.Level.Scripts.Goal
{
  public class LevelGoal
  {
    private readonly GameGoalRanges ranges;

    public LevelGoal(GameGoalRanges ranges)
    {
      this.ranges = ranges;
    }

    public int MaxPoints() => 
      ranges.Percents[^1].MaxPointCount;

    public bool IsPass(int currentPoints) => 
      currentPoints * 1f / MaxPoints() > ranges.MinPercentToPassGame;

    public float PassPercent(int currentPoints)
    {
      for (int i = 0; i < ranges.Percents.Length; i++)
      {
        if (ranges.Percents[i].MinPointCount <= currentPoints && ranges.Percents[i].MaxPointCount >= currentPoints)
          return ranges.Percents[i].PassPercent;
      }

      return 0f;
    }
  }
}