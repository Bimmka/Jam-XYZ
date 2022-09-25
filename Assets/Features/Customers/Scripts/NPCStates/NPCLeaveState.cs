using Features.Animation;
using Features.Customers.Scripts.Base;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCLeaveState : NPCStateMachineState
  {
    public NPCLeaveState(NPCStateMachineObserver npc, SimpleAnimator animator) : base(npc, animator)
    {
    }
  }
}