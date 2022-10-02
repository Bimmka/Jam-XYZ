using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceIdleState : PoliceStateMachineState
  {
    public PoliceIdleState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}