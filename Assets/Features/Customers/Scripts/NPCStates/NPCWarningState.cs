using Features.Alarm;
using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.StaticData.LevelArea;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCWarningState : NPCStateMachineState
  {
    private readonly NPCAlertnessObserver alertnessObserver;
    private readonly NPCAlarm alarm;
    private readonly LevelAreaType area;

    public NPCWarningState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName, NPCAlertnessObserver alertnessObserver,
      NPCAlarm alarm, LevelAreaType area) : base(npc, animator, animationName)
    {
      this.alertnessObserver = alertnessObserver;
      this.alarm = alarm;
      this.area = area;
    }

    public override void Enter()
    {
      base.Enter();
      alertnessObserver.HideAlertnessTip();
      alertnessObserver.ShowWaryTip();
      alarm.InvokeAlarm(area, Position);
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