using Features.Animation;
using Features.Customers.Scripts.Base;
using Pathfinding;
using UnityEngine;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCMoveState : NPCStateMachineState
  {
    private readonly AIDestinationSetter destinationSetter;
    private readonly AIPath aiPath;
    private Transform moveTarget;

    public NPCMoveState(NPCStateMachineObserver npc, SimpleAnimator animator, AIDestinationSetter destinationSetter, AIPath aiPath) : base(npc, animator)
    {
      this.destinationSetter = destinationSetter;
      this.aiPath = aiPath;
    }

    public override void Enter()
    {
      base.Enter();
      destinationSetter.target = moveTarget;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);

      if (IsReachTarget())
        SetIdleState();
    }

    public override void Exit()
    {
      base.Exit();
      destinationSetter.target = null;
      moveTarget = null;
    }

    public void SaveFinishPosition(Transform newTarget) => 
      moveTarget = newTarget;

    private bool IsReachTarget() => 
      aiPath.reachedDestination;

    private void SetIdleState() => 
      ChangeState<NPCIdleState>();
  }
}