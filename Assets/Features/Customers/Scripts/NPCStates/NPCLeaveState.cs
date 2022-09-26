using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.LevelArea.Scripts.PointsOfInterest;
using Features.StaticData.LevelArea;
using Pathfinding;
using UnityEngine;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCLeaveState : NPCStateMachineState
  {
    private readonly LevelPointsOfInterestObserver pointsOfInterestObserver;
    private readonly AIPath path;
    private readonly AIDestinationSetter destinationSetter;
    private readonly LevelAreaType area;

    private Transform exitTarget;

    public NPCLeaveState(NPCStateMachineObserver npc, SimpleAnimator animator, string animationName, LevelPointsOfInterestObserver pointsOfInterestObserver, 
      AIPath path, AIDestinationSetter destinationSetter, LevelAreaType area) : base(npc, animator, animationName)
    {
      this.pointsOfInterestObserver = pointsOfInterestObserver;
      this.path = path;
      this.destinationSetter = destinationSetter;
      this.area = area;
    }

    public override void Enter()
    {
      base.Enter();
      exitTarget = pointsOfInterestObserver.NearestExit(area, Position);
      destinationSetter.target = exitTarget;
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