using Features.Animation;
using Features.Customers.Scripts.Base;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCWarningState : NPCStateMachineState
  {
    public NPCWarningState(NPCStateMachineObserver npc, SimpleAnimator animator) : base(npc, animator)
    {
    }
  }
}