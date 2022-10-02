using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceWarnedState : PoliceStateMachineState
  {
    public PoliceWarnedState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}