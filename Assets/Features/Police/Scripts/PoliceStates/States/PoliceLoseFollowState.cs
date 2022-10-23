using Features.Animation;
using Features.Police.Scripts.Searching;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceLoseFollowState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;
    private readonly float loseFollowDuration;

    private float currentLoseFollowDuration;

    public PoliceLoseFollowState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher, float loseFollowDuration) : base(police, animator, animationName)
    {
      this.searcher = searcher;
      this.loseFollowDuration = loseFollowDuration;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsFoundPlayer())
        StartFollow();
      else if (IsNeedToSearch())
        StartSearch();
      else
        UpdateWaitDuration(deltaTime);
    }

    private void UpdateWaitDuration(float deltaTime) => 
      currentLoseFollowDuration += deltaTime;

    private void StartFollow() => 
      ChangeState<PoliceFollowState>();

    private void StartSearch()
    {
      PoliceSearchState state = State<PoliceSearchState>();
      state.RecalculateNearestPoint();
      ChangeState(state);
    }

    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;

    private bool IsNeedToSearch() => 
      currentLoseFollowDuration >= loseFollowDuration;
  }
}