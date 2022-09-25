using Features.Animation;
using Features.NPC.Scripts.Base;

namespace Features.NPC.Scripts.NPCStateMachine
{
  public class NPCWarningState : NPCStateMachineState
  {
    public NPCWarningState(NPCStateMachineObserver npc, SimpleAnimator animator) : base(npc, animator)
    {
    }
  }
}