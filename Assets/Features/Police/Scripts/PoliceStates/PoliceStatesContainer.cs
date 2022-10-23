using System;
using System.Collections.Generic;
using Features.Animation;
using Features.Police.Data;
using Features.Police.Scripts.Path;
using Features.Police.Scripts.PoliceStates.States;
using Features.Police.Scripts.Searching;
using Pathfinding;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates
{
  public class PoliceStatesContainer
  {
    private readonly PoliceStateMachineObserver stateMachine;
    private readonly SimpleAnimator animator;
    private readonly PoliceHeroSearcher searcher;
    private readonly AIPath aiPath;
    private readonly AIDestinationSetter destinationSetter;
    private readonly PolicePathObserver pathObserver;
    private readonly PoliceSettings settings;
    private readonly Dictionary<Type, PoliceStateMachineState> states;

    public PoliceStatesContainer(PoliceStateMachineObserver stateMachine, SimpleAnimator animator, PoliceHeroSearcher searcher,
      AIPath aiPath, AIDestinationSetter destinationSetter, PolicePathObserver pathObserver, PoliceSettings settings)
    {
      this.stateMachine = stateMachine;
      this.animator = animator;
      this.searcher = searcher;
      this.aiPath = aiPath;
      this.destinationSetter = destinationSetter;
      this.pathObserver = pathObserver;
      this.settings = settings;
      states = new Dictionary<Type, PoliceStateMachineState>(9);
    }
    
    public void CreateStates()
    {
      CreateIdleState();
      CreateWarnedIdleState();
      CreatePatrolState();
      CreateSearchState();
      CreateFollowState();
      CreateWarnedState();
      CreateLoseFollowState();
      CreateNPCAskState();
      CreateArestingState();
    }

    public TState GetState<TState>() where TState : PoliceStateMachineState=> 
      (TState) states[typeof(TState)];

    private void CreateIdleState()
    {
      PoliceIdleState state = new PoliceIdleState(stateMachine, animator, "IsIdle", searcher);
      SaveState(state);
    }

    private void CreateWarnedIdleState()
    {
      PoliceWarnedIdleState state = new PoliceWarnedIdleState(stateMachine, animator, "IsWarnedIdle", searcher, settings.PointsInSearching);
      SaveState(state);
    }

    private void CreatePatrolState()
    {
      PolicePatrolState state = new PolicePatrolState(stateMachine, animator, "IsPatrol", searcher, aiPath, pathObserver, 
        settings.PatrolSpeed);
      SaveState(state);
    }

    private void CreateSearchState()
    {
      PoliceSearchState state = new PoliceSearchState(stateMachine, animator, "IsSearch", searcher, aiPath, pathObserver,
        settings.SearchSpeed);
      SaveState(state);
    }

    private void CreateFollowState()
    {
      PoliceFollowState state = new PoliceFollowState(stateMachine, animator, "IsFollow", aiPath, destinationSetter, searcher,
        settings.ArrestDistance, settings.FollowSpeed);
      SaveState(state);
    }

    private void CreateWarnedState()
    {
      PoliceWarnedState state = new PoliceWarnedState(stateMachine, animator, "IsWarned", searcher, aiPath,
        settings.WarnedSpeed);
      SaveState(state);
    }

    private void CreateLoseFollowState()
    {
      PoliceLoseFollowState state = new PoliceLoseFollowState(stateMachine, animator, "IsLoseFollow", 
        searcher, settings.LoseFollowWaitDuration);
      SaveState(state);
    }

    private void CreateNPCAskState()
    {
      PoliceAskingNPCState state = new PoliceAskingNPCState(stateMachine, animator, "IsAsking", searcher);
      SaveState(state);
    }

    private void CreateArestingState()
    {
      PoliceArrestingState state = new PoliceArrestingState(stateMachine, animator, "IsArresting");
      SaveState(state);
    }

    private void SaveState(PoliceStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}