using System;
using System.Collections.Generic;
using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.LevelArea.Scripts.PointsOfInterest;
using Features.StaticData.LevelArea;
using Pathfinding;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCStatesContainer
  {
    private readonly NPCStateMachineObserver npc;
    private readonly SimpleAnimator animator;
    private readonly AIPath aiPath;
    private readonly AIDestinationSetter aiDestinationSetter;
    private readonly NPCAlertnessObserver alertness;
    private readonly LevelPointsOfInterestObserver pointsOfInterestObserver;
    private readonly LevelAreaType area;
    private readonly Seeker seeker;
    private readonly Dictionary<Type, NPCStateMachineState> states;

    public NPCStatesContainer(NPCStateMachineObserver npc, SimpleAnimator animator, AIPath aiPath,
      AIDestinationSetter aiDestinationSetter, NPCAlertnessObserver alertness, LevelPointsOfInterestObserver pointsOfInterestObserver, 
      LevelAreaType area)
    {
      this.npc = npc;
      this.animator = animator;
      this.aiPath = aiPath;
      this.aiDestinationSetter = aiDestinationSetter;
      this.alertness = alertness;
      this.pointsOfInterestObserver = pointsOfInterestObserver;
      this.area = area;
      states = new Dictionary<Type, NPCStateMachineState>(5);
    }
    public void CreateStates()
    {
      CreateIdleState();
      CreateLeaveState();
      CreateMoveState();
      CreateWarningState();
    }

    public TState GetState<TState>() where TState : NPCStateMachineState => 
      (TState) states[typeof(TState)];

    private void CreateIdleState()
    {
      NPCIdleState state = new NPCIdleState(npc, animator, "IsIdle", alertness);
      SaveState(state);
    }

    private void CreateLeaveState()
    {
      NPCLeaveState state = new NPCLeaveState(npc, animator, "IsMove", pointsOfInterestObserver, aiPath, aiDestinationSetter, area);
      SaveState(state);
    }

    private void CreateMoveState()
    {
      NPCMoveState state = new NPCMoveState(npc, animator, "IsMove", aiDestinationSetter, aiPath);
      SaveState(state);
    }

    private void CreateWarningState()
    {
      NPCWarningState state = new NPCWarningState(npc, animator, "IsWary", alertness);
      SaveState(state);
    }

    private void SaveState(NPCStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}