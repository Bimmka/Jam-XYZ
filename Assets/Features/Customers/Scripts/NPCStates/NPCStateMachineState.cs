using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.StateMachines;
using UnityEngine;

namespace Features.Customers.Scripts.NPCStates
{
  public abstract class NPCStateMachineState : BaseStateMachineState, IUpdatableState
  {
    protected readonly NPCStateMachineObserver npc;
    private readonly SimpleAnimator animator;

    private int hashedAnimation;

    protected Vector3 Position => npc.transform.position;

    protected NPCStateMachineState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName)
    {
      this.npc = npc;
      this.animator = animator;

      hashedAnimation = Animator.StringToHash(animationName);
    }

    public override void Enter()
    {
      base.Enter();
      SetBool(hashedAnimation, true);
    }

    public virtual void UpdateState(in float deltaTime) { }

    public override void Exit()
    {
      base.Exit();
      SetBool(hashedAnimation, false);
    }

    protected void SetBool(int hash, bool value) => 
      animator.SetBool(hash, value);

    protected void ChangeState<TState>() where TState : NPCStateMachineState => 
      npc.ChangeState<TState>();
  }
}