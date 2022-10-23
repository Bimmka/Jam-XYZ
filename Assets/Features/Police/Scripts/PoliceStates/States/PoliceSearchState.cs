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

    public PoliceSearchState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher, AIPath aiPath, PolicePathObserver pathObserver) : base(police, animator, animationName)
    {
      this.searcher = searcher;
      this.aiPath = aiPath;
      this.pathObserver = pathObserver;
    }

    public override void Enter()
    {
      base.Enter();

      aiPath.destination = pathObserver.SearchPosition();
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      
      if (IsFoundPlayer())
        FollowPlayer();
      else if (IsReachTarget())
        StartWait();
    }

    public void RecalculateNearestPoint() => 
      pathObserver.RecalculateNearestSearchPosition(police.transform.position);

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