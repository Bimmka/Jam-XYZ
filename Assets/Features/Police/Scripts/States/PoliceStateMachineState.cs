using Features.Animation;
using Features.StateMachines;
using UnityEngine;

namespace Features.Police.Scripts.States
{
  public class PoliceStateMachineState : BaseStateMachineState
  {
    private readonly PoliceStateMachineObserver police;
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

    public override void Exit()
    {
      base.Exit();
      animator.SetBool(hashedAnimation, false);
    }
  }
}