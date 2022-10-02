using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceSearchState: PoliceStateMachineState
  {
    public PoliceSearchState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}