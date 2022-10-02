using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceWarnedIdleState : PoliceStateMachineState
  {
    public PoliceWarnedIdleState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}