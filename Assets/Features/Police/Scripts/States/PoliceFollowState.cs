using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceFollowState : PoliceStateMachineState
  {
    public PoliceFollowState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}