﻿using Features.Animation;
using Features.NPC.Scripts.Base;

namespace Features.NPC.Scripts.NPCStates
{
  public class NPCIdleState : NPCStateMachineState
  {
    public NPCIdleState(NPCStateMachineObserver npc, SimpleAnimator animator) : base(npc, animator)
    {
    }
  }
}