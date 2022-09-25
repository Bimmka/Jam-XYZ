using Features.Animation;
using Features.NPC.Scripts.Base;

namespace Features.NPC.Scripts.NPCStates
{
  public class NPCMoveState : NPCStateMachineState
  {
    public NPCMoveState(NPCStateMachineObserver npc, SimpleAnimator animator) : base(npc, animator)
    {
    }
  }
}