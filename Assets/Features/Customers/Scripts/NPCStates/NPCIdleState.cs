using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCIdleState : NPCStateMachineState
  {
    private readonly NPCAlertnessObserver alertnessObserver;

    public NPCIdleState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName, NPCAlertnessObserver alertnessObserver) : base(npc, animator, animationName)
    {
      this.alertnessObserver = alertnessObserver;
    }

    public override void Enter()
    {
      base.Enter();
      alertnessObserver.ChangeStealableState(true);
    }

    public override void Exit()
    {
      base.Exit();
      alertnessObserver.ChangeStealableState(false);
    }
  }
}