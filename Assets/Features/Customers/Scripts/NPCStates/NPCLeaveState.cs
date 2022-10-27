using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.Level.Scripts.PointsOfInterest;
using Features.StaticData.LevelArea;
using Pathfinding;
using UnityEngine;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCLeaveState : NPCStateMachineState
  {
    private readonly LevelPointsOfInterestObserver pointsOfInterestObserver;
    private readonly AIPath path;
    private readonly LevelAreaType area;

    public NPCLeaveState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName, LevelPointsOfInterestObserver pointsOfInterestObserver, 
      AIPath path, LevelAreaType area) : base(npc, animator, animationName)
    {
      this.pointsOfInterestObserver = pointsOfInterestObserver;
      this.path = path;
      this.area = area;
    }

    public override void Enter()
    {
      base.Enter();
      path.destination = pointsOfInterestObserver.NearestExit(area, Position);
      path.enabled = true;
    }

    public override void UpdateState(in float deltaTime)
    {
      base.UpdateState(in deltaTime);
      
      if (path.reachedDestination)
        Exit();
    }

    public override void Exit()
    {
      base.Exit();
      npc.NotifyAboutLeave();
      path.enabled = false;
    }
  }
}