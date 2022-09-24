using UniRx;

namespace Features.Player.Scripts.Gold
{
  public class HeroGold
  {
    public readonly IntReactiveProperty Count = new IntReactiveProperty();

    public void Add(int count)
    {
      Count.Value += count;
    }
  }
}