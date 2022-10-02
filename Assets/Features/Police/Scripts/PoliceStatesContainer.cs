using System;
using System.Collections.Generic;
using Features.Animation;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Police.Scripts.States;

namespace Features.Police.Scripts
{
  public class PoliceStatesContainer
  {
    private readonly PoliceStateMachineObserver stateMachine;
    private readonly SimpleAnimator animator;
    private Dictionary<Type, PoliceStateMachineState> states;

    public PoliceStatesContainer(PoliceStateMachineObserver stateMachine, SimpleAnimator animator)
    {
      this.stateMachine = stateMachine;
      this.animator = animator;
      states = new Dictionary<Type, PoliceStateMachineState>(9);
    }
    
    public void CreateStates()
    {
      CreateIdleState();
      CreateWarnedIdleState();
      CreatePatrulState();
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
      PoliceIdleState state = new PoliceIdleState(stateMachine, animator, "IsIdle");
      SaveState(state);
    }

    private void CreateWarnedIdleState()
    {
      PoliceWarnedIdleState state = new PoliceWarnedIdleState(stateMachine, animator, "IsWarnedIdle");
      SaveState(state);
    }

    private void CreatePatrulState()
    {
      PolicePatrulState state = new PolicePatrulState(stateMachine, animator, "IsPatrul");
      SaveState(state);
    }

    private void CreateSearchState()
    {
      PoliceSearchState state = new PoliceSearchState(stateMachine, animator, "IsSearch");
      SaveState(state);
    }

    private void CreateFollowState()
    {
      PoliceFollowState state = new PoliceFollowState(stateMachine, animator, "IsFollow");
      SaveState(state);
    }

    private void CreateWarnedState()
    {
      PoliceWarnedState state = new PoliceWarnedState(stateMachine, animator, "IsWarned");
      SaveState(state);
    }

    private void CreateLoseFollowState()
    {
      PoliceLoseFollowState state = new PoliceLoseFollowState(stateMachine, animator, "IsLoseFollow");
      SaveState(state);
    }

    private void CreateNPCAskState()
    {
      PoliceAskingNPCState state = new PoliceAskingNPCState(stateMachine, animator, "IsAsking");
      SaveState(state);
    }

    private void CreateArestingState()
    {
      PoliceArestingState state = new PoliceArestingState(stateMachine, animator, "IsAresting");
      SaveState(state);
    }

    private void SaveState(PoliceStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}