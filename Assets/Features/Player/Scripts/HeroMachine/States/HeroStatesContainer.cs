using System;
using System.Collections.Generic;
using Features.Alarm;
using Features.Animation;
using Features.Player.Scripts.Gold;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.HeroMachine.States.Interaction;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Steal;
using Features.Services.UI.Windows;
using Features.StateMachines;
using Features.StaticData.Hero.AnimationTransitions;
using Zenject;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroStatesContainer
  {
    private readonly HeroStateMachineObserver hero;
    private readonly HeroMove move;
    private readonly ChangeableParametersAnimator animator;
    private readonly HeroAnimationsTransitionStaticData transitionStaticData;

    private readonly Dictionary<Type, BaseStateMachineState> states;
    private readonly HeroNPCSearcher npcSearcher;
    private readonly HeroStealPreparing stealPreparing;
    private readonly IWindowsService windowsService;
    private readonly HeroGold heroGold;
    private NPCAlarm alarm;

    [Inject]
    public HeroStatesContainer(HeroStateMachineObserver hero, HeroMove move, ChangeableParametersAnimator animator,
      HeroAnimationsTransitionStaticData transitionStaticData, HeroNPCSearcher npcSearcher, HeroStealPreparing stealPreparing,
      IWindowsService windowsService, HeroGold heroGold, NPCAlarm alarm)
    {
      this.hero = hero;
      this.move = move;
      this.animator = animator;
      this.transitionStaticData = transitionStaticData;
      this.npcSearcher = npcSearcher;
      this.stealPreparing = stealPreparing;
      this.windowsService = windowsService;
      this.heroGold = heroGold;
      this.alarm = alarm;

      states = new Dictionary<Type, BaseStateMachineState>(20);
    }

    public void CreateStates()
    {
      CreateIdleState();
      CreateRunState();
      CreateInteractionPrepareState();
      CreateInteractionState();
    }

    public TState GetState<TState>() where TState : BaseStateMachineState
    {
      if (states.ContainsKey(typeof(TState)))
        return (TState)states[typeof(TState)];

      throw new ArgumentNullException();
    }

    private void CreateIdleState()
    {
      HeroIdleState state = new HeroIdleState(hero, (FloatAnimationTransitionStaticData) transitionStaticData.Transitions["Idle"], animator, npcSearcher);
      SaveState(state);
    }

    private void CreateRunState()
    {
      HeroMoveState state = new HeroMoveState(hero,move, (FloatAnimationTransitionStaticData)transitionStaticData.Transitions["Run"], animator);
      SaveState(state);
    }

    private void CreateInteractionPrepareState()
    {
      HeroInteractionPrepareState state = new HeroInteractionPrepareState(hero, 
        transitionStaticData.Transitions["InteractionPrepare"], 
        animator, stealPreparing, npcSearcher, alarm);
      SaveState(state);
    }
    
    private void CreateInteractionState()
    {
      HeroInteractionState state = new HeroInteractionState(hero, animator, npcSearcher, stealPreparing, windowsService, heroGold);
      SaveState(state);
    }

    private void SaveState(BaseStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}