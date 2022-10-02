using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceArestingState : PoliceStateMachineState
  {
    public PoliceArestingState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}