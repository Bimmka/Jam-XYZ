using Features.Animation;
using Features.Police.Scripts.Searching;
using Pathfinding;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceWarnedState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;
    private readonly AIPath aiPath;
    private readonly float moveSpeed;

    private Vector3 alarmPosition;

    public PoliceWarnedState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher, AIPath aiPath, float moveSpeed) : base(police, animator, animationName)
    {
      this.searcher = searcher;
      this.aiPath = aiPath;
      this.moveSpeed = moveSpeed;
    }

    public override void Enter()
    {
      base.Enter();
      aiPath.maxSpeed = moveSpeed;
      aiPath.destination = alarmPosition;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);

      if (IsFoundPlayer())
        FollowPlayer();
      else if (IsReachNPC())
        AskNPC();
    }

    public void SaveInvokePosition(Vector3 position) => 
      alarmPosition = position;

    private void FollowPlayer() => 
      ChangeState<PoliceFollowState>();

    private void AskNPC() => 
      ChangeState<PoliceAskingNPCState>();

    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;

    private bool IsReachNPC() => 
      aiPath.reachedDestination;
  }
}