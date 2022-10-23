using Features.Animation;
using Features.Police.Scripts.Searching;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceLoseFollowState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;

    public PoliceLoseFollowState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher) : base(police, animator, animationName)
    {
      this.searcher = searcher;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsFoundPlayer())
        StartFollow();
      else
        StartSearch();
    }
    
    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;

    private void StartFollow() => 
      ChangeState<PoliceFollowState>();
    
    private void StartSearch()
    {
      PoliceSearchState state = State<PoliceSearchState>();
      state.RecalculateNearestPoint();
      ChangeState(state);
    }
  }
}