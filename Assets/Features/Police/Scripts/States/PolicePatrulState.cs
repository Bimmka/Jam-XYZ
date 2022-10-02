using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PolicePatrulState : PoliceStateMachineState
  {
    public PolicePatrulState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}