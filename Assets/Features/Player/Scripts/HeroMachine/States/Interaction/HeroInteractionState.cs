using Features.Animation;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.Steal;

namespace Features.Player.Scripts.HeroMachine.States.Interaction
{
  public class HeroInteractionState : HeroStateMachineState
  {
    private readonly HeroNPCSearcher npcSearcher;
    private readonly HeroStealPreparing stealPreparing;

    public HeroInteractionState(HeroStateMachineObserver hero, ChangeableParametersAnimator animator, HeroNPCSearcher npcSearcher, 
      HeroStealPreparing stealPreparing) : base(hero, animator)
    {
      this.npcSearcher = npcSearcher;
      this.stealPreparing = stealPreparing;
    }

    public override void Enter()
    {
      base.Enter();
      stealPreparing.ResetPreparing();
      ChangeState<HeroIdleState>();
    }

    public override void Exit()
    {
      base.Exit();
      npcSearcher.StartSearch();
    }
  }
}