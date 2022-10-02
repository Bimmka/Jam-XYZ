using Features.Animation;

namespace Features.Police.Scripts.States
{
  public class PoliceAskingNPCState : PoliceStateMachineState
  {
    public PoliceAskingNPCState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }
  }
}