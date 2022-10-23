using Features.Animation;
using Features.Police.Scripts.Searching;
using Pathfinding;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceWarnedState : PoliceStateMachineState
  {
    private readonly PoliceHeroSearcher searcher;
    private readonly AIPath aiPath;

    public PoliceWarnedState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      PoliceHeroSearcher searcher, AIPath aiPath) : base(police, animator, animationName)
    {
      this.searcher = searcher;
      this.aiPath = aiPath;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);

      if (IsFoundPlayer())
        FollowPlayer();
      else if (IsReachNPC())
        AskNPC();
    }

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