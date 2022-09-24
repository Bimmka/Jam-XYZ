using Features.Animation;

namespace Features.Player.Scripts.HeroMachine.States.Interaction
{
  public class HeroInteractionState : HeroStateMachineState
  {
    public HeroInteractionState(HeroStateMachineObserver hero, ChangeableParametersAnimator animator) : base(hero, animator)
    {
    }
  }
}