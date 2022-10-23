using Features.Animation;
using Features.Police.Scripts.Searching;
using Pathfinding;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceFollowState : PoliceStateMachineState
  {
    private readonly AIPath aiPath;
    private readonly AIDestinationSetter destinationSetter;
    private readonly PoliceHeroSearcher searcher;
    private Transform player;

    public PoliceFollowState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      AIPath aiPath, AIDestinationSetter destinationSetter, PoliceHeroSearcher searcher)
      : base(police, animator, animationName)
    {
      this.aiPath = aiPath;
      this.destinationSetter = destinationSetter;
      this.searcher = searcher;
    }

    public override void Enter()
    {
      base.Enter();
      destinationSetter.target = searcher.PlayerHit.transform;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsReachPlayer())
        Arrest();
    }

    public void LoseFollow() => 
      ChangeState<PoliceLoseFollowState>();

    private void Arrest()
    {
      aiPath.enabled = false;
      ChangeState<PoliceArrestingState>();
    }

    private bool IsReachPlayer() => 
      aiPath.reachedDestination;
  }
}