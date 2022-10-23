using Features.Animation;
using Features.Police.Scripts.Searching;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceWarnedIdleState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;
    private readonly int maxPointsInLine;

    private int currentPointsInLine;

    public PoliceWarnedIdleState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher, int maxPointsInLine) : base(police, animator, animationName)
    {
      this.searcher = searcher;
      this.maxPointsInLine = maxPointsInLine;
    }

    public override void Enter()
    {
      base.Enter();
      currentPointsInLine++;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsFoundPlayer())
      {
        StartFollow();
        ResetSearch();
      }
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      if (IsNeedToPatrol())
      {
        ResetSearch();
        ContinuePatrol();
      }
      else
        ContinueSearch();
    }

    public void ResetSearch() => 
      currentPointsInLine = 0;

    private void ContinuePatrol()
    {
      PolicePatrolState patrolState = State<PolicePatrolState>();
      patrolState.RecalculateNearestPoint();
      ChangeState(patrolState);
    }

    private bool IsNeedToPatrol() => 
      currentPointsInLine >= maxPointsInLine;

    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;

    private void StartFollow() => 
      ChangeState<PoliceFollowState>();

    private void ContinueSearch() => 
      ChangeState<PoliceSearchState>();
  }
}