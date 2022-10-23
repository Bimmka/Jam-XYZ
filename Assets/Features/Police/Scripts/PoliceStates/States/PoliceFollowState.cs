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
    private readonly float arrestDistance;
    private readonly float moveSpeed;

    public PoliceFollowState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName,
      AIPath aiPath, AIDestinationSetter destinationSetter, PoliceHeroSearcher searcher, float arrestDistance, float moveSpeed)
      : base(police, animator, animationName)
    {
      this.aiPath = aiPath;
      this.destinationSetter = destinationSetter;
      this.searcher = searcher;
      this.arrestDistance = arrestDistance;
      this.moveSpeed = moveSpeed;
    }

    public override void Enter()
    {
      base.Enter();
      aiPath.maxSpeed = moveSpeed;
      destinationSetter.target = searcher.PlayerHit.transform;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      if (IsCanArrest())
        Arrest();
    }

    public void LoseFollow() => 
      ChangeState<PoliceLoseFollowState>();

    private void Arrest()
    {
      aiPath.enabled = false;
      ChangeState<PoliceArrestingState>();
    }

    private bool IsCanArrest() => 
      Vector3.Distance(police.transform.position, searcher.PlayerHit.transform.position) <= arrestDistance;
  }
}