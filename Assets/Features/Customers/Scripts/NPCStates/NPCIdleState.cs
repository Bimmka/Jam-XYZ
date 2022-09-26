﻿using Features.Animation;
using Features.Customers.Scripts.Base;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCIdleState : NPCStateMachineState
  {
    public NPCIdleState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName) : base(npc, animator, animationName)
    {
    }
  }
}