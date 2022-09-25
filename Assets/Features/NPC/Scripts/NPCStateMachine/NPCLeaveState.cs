using Features.Animation;
using Features.NPC.Scripts.Base;

namespace Features.NPC.Scripts.NPCStateMachine
{
  public class NPCLeaveState : NPCStateMachineState
  {
    public NPCLeaveState(NPCStateMachineObserver npc, SimpleAnimator animator) : base(npc, animator)
    {
    }
  }
}