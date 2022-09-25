using Features.Animation;

namespace Features.Player.Scripts.HeroMachine.States.Interaction
{
  public class HeroInteractionPrepareState : HeroStateMachineState
  {
    public HeroInteractionPrepareState(HeroStateMachineObserver hero, ChangeableParametersAnimator animator) : base(hero, animator)
    {
    }
  }
}