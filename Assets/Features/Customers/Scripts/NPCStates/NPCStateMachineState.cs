using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.StateMachines;
using UnityEngine;

namespace Features.Customers.Scripts.NPCStates
{
  public abstract class NPCStateMachineState : BaseStateMachineState
  {
    private readonly NPCStateMachineObserver npc;
    private readonly SimpleAnimator animator;

    protected Vector3 Position => npc.transform.position;

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

    protected void ChangeState<TState>() where TState : NPCStateMachineState => 
      npc.ChangeState<TState>();
  }
}