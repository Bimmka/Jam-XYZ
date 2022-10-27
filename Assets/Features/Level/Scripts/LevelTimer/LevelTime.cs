namespace Features.Level.Scripts.LevelTimer
{
  public struct LevelTime
  {
    public int Current;
    public int Max;
    public LevelTime(int current, int max)
    {
      Current = current;
      Max = max;
    }
  }
}