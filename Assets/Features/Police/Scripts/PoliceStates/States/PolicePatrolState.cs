using Features.Animation;
using Features.Police.Scripts.Path;
using Features.Police.Scripts.Searching;
using Pathfinding;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PolicePatrolState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;
    private readonly AIPath aiPath;
    private readonly PolicePathObserver pathObserver;
    private readonly float moveSpeed;

    public PolicePatrolState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
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
      aiPath.destination = pathObserver.PatrolPosition();
      pathObserver.IncPatrolIndex();
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);

      if (IsFoundStealingPlayer())
        FollowPlayer();
      else if (IsReachTarget())
        StartWait();
    }

    public void RecalculateNearestPoint() => 
      pathObserver.RecalculateNearestPatrolPosition(police.transform.position);

    private void FollowPlayer() => 
      ChangeState<PoliceFollowState>();

    private void StartWait() => 
      ChangeState<PoliceIdleState>();

    private bool IsFoundStealingPlayer() => 
      searcher.PlayerHit != null && searcher.PlayerHit.IsStealing;

    private bool IsReachTarget() => 
      aiPath.reachedDestination;
  }
}