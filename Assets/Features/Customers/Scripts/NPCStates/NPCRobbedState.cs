using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.Customers.Scripts.Timing;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCRobbedState : NPCStateMachineState
  {
    private readonly NPCExistTimeObserver existTimeObserver;

    public NPCRobbedState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName, NPCExistTimeObserver existTimeObserver) 
      : base(npc, animator, animationName)
    {
      this.existTimeObserver = existTimeObserver;
    }

    public override void Enter()
    {
      base.Enter();
      existTimeObserver.StopTimer();
      ChangeState<NPCLeaveState>();
    }
  }
}