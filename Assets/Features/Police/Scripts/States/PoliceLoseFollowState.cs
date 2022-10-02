using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceLoseFollowState : PoliceStateMachineState
  {
    public PoliceLoseFollowState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}