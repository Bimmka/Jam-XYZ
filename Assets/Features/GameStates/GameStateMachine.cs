﻿using System;
using System.Collections.Generic;
using Features.GameStates.States;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Zenject;

namespace Features.GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    [Inject]
    public GameStateMachine(BootstrapState bootstrapState, GameLoadState gameLoadState)
    {
      _states = new Dictionary<Type, IExitableState>(5);
      _states.Add(bootstrapState.GetType(), bootstrapState);
      _states.Add(gameLoadState.GetType(), gameLoadState);
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, loadedCallback, curtainHideCallback);
    }

    public TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;


    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    public void Cleanup()
    {
      
    }
  }
}