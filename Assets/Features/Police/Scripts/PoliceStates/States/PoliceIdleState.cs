using Features.Animation;
using Features.Police.Scripts.Searching;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceIdleState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;

    public PoliceIdleState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher) : base(police, animator, animationName)
    {
      this.searcher = searcher;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsFoundStealingPlayer())
        StartFollow();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      ContinuePatrol();
    }

    private bool IsFoundStealingPlayer() => 
      searcher.PlayerHit != null && searcher.PlayerHit.IsStealing;

    private void StartFollow() => 
      ChangeState<PoliceFollowState>();

    private void ContinuePatrol() => 
      ChangeState<PolicePatrolState>();
  }
}