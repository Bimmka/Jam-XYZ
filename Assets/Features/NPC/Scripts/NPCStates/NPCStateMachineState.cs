﻿using Features.Animation;
using Features.NPC.Scripts.Base;
using Features.StateMachines;

namespace Features.NPC.Scripts.NPCStates
{
  public abstract class NPCStateMachineState : BaseStateMachineState
  {
    private readonly NPCStateMachineObserver npc;
    private readonly SimpleAnimator animator;

    protected NPCStateMachineState(NPCStateMachineObserver npc, SimpleAnimator animator)
    {
      this.npc = npc;
      this.animator = animator;
    }

    public virtual void UpdateState(in float deltaTime)
    {
      
    }

    protected void SetBool(int hash, bool value) => 
      animator.SetBool(hash, value);
  }
}