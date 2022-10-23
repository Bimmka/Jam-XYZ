using Features.Animation;
using Features.Police.Scripts.Path;
using Features.Police.Scripts.Searching;
using Pathfinding;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceSearchState: PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;
    private readonly AIPath aiPath;
    private readonly PolicePathObserver pathObserver;
    private readonly float moveSpeed;

    public PoliceSearchState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher, AIPath aiPath, PolicePathObserver pathObserver, float moveSpeed) : base(police, animator, animationName)
    {
      this.searcher = searcher;
      this.aiPath = aiPath;
      this.pathObserver = pathObserver;
      this.moveSpeed = moveSpeed;
    }

    public override void Enter()
    {
      base.Enter();

      aiPath.maxSpeed = moveSpeed;
      aiPath.destination = pathObserver.SearchPosition();
      pathObserver.IncSearchIndex();
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);

      if (IsFoundPlayer())
      {
        FollowPlayer();
        ResetSearch();
      }
      else if (IsReachTarget())
        StartWait();
    }

    public void RecalculateNearestPoint() => 
      pathObserver.RecalculateNearestSearchPosition(police.transform.position);

    private void ResetSearch() => 
      State<PoliceWarnedIdleState>().ResetSearch();

    private void FollowPlayer() => 
      ChangeState<PoliceFollowState>();

    private void StartWait() => 
      ChangeState<PoliceWarnedIdleState>();

    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;

    private bool IsReachTarget() => 
      aiPath.reachedDestination;
  }
}