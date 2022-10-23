using Features.Animation;
using Features.Customers.Scripts.NPCStates;
using Features.StateMachines;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceStateMachineState : BaseStateMachineState, IUpdatableState
  {
    protected readonly PoliceStateMachineObserver police;
    private readonly SimpleAnimator animator;
    
    private readonly int hashedAnimation;
    
    public PoliceStateMachineState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName)
    {
      this.police = police;
      this.animator = animator;

      hashedAnimation = Animator.StringToHash(animationName);
    }

    public override void Enter()
    {
      base.Enter();
      animator.SetBool(hashedAnimation, true);
    }
    
    public virtual void UpdateState(in float deltaTime) { }

    public override void Exit()
    {
      base.Exit();
      animator.SetBool(hashedAnimation, false);
    }

    protected void ChangeState<TState>() where TState : PoliceStateMachineState => 
      police.ChangeState<TState>();

    protected TState State<TState>() where TState : PoliceStateMachineState => 
      police.State<TState>();

    protected void ChangeState<TState>(TState state) where TState : PoliceStateMachineState => 
      police.ChangeState(state);
  }
}