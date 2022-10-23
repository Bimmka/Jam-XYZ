using Features.Animation;
using Features.Police.Scripts.Searching;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceWarnedIdleState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;

    public PoliceWarnedIdleState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher) : base(police, animator, animationName)
    {
      this.searcher = searcher;
    }
    
    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsFoundPlayer())
        StartFollow();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      ContinueSearch();
    }

    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;

    private void StartFollow() => 
      ChangeState<PoliceFollowState>();

    private void ContinueSearch() => 
      ChangeState<PoliceSearchState>();
  }
}