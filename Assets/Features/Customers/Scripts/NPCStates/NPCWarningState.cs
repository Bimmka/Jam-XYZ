using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCWarningState : NPCStateMachineState
  {
    private readonly NPCAlertnessObserver alertnessObserver;

    public NPCWarningState(NPCStateMachineObserver npc, SimpleAnimator animator, NPCAlertnessObserver alertnessObserver) : base(npc, animator)
    {
      this.alertnessObserver = alertnessObserver;
    }

    public override void Enter()
    {
      base.Enter();
      alertnessObserver.HideAlertnessTip();
      alertnessObserver.ShowWaryTip();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      ChangeState<NPCLeaveState>();
    }

    public override void Exit()
    {
      base.Exit();
      alertnessObserver.HideWaryTip();
    }
  }
}