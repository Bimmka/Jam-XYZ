using Features.Animation;
using Features.Police.Scripts.Searching;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceAskingNPCState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;

    public PoliceAskingNPCState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher) : base(police, animator, animationName)
    {
      this.searcher = searcher;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);

      if (IsFoundPlayer())
        FollowPlayer();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      StartSearch();
    }

    private void FollowPlayer() => 
      ChangeState<PoliceFollowState>();

    private void StartSearch()
    {
      PoliceSearchState state = State<PoliceSearchState>();
      state.RecalculateNearestPoint();
      ChangeState(state);
    }

    private bool IsFoundPlayer() =>
      searcher.PlayerHit != null;
  }
}