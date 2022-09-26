using System;
using System.Collections.Generic;
using Features.Animation;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.HeroMachine.States.Interaction;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Steal;
using Features.StateMachines;
using Features.StaticData.Hero.AnimationTransitions;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroStatesContainer
  {
    private readonly HeroStateMachineObserver hero;
    private readonly HeroMove move;
    private readonly ChangeableParametersAnimator animator;
    private readonly HeroAnimationsTransitionStaticData transitionStaticData;

    private readonly Dictionary<Type, BaseStateMachineState> states;
    private HeroNPCSearcher npcSearcher;
    private readonly HeroStealPreparing stealPreparing;

    public HeroStatesContainer(HeroStateMachineObserver hero, HeroMove move, ChangeableParametersAnimator animator,
      HeroAnimationsTransitionStaticData transitionStaticData, HeroNPCSearcher npcSearcher, HeroStealPreparing stealPreparing)
    {
      this.hero = hero;
      this.move = move;
      this.animator = animator;
      this.transitionStaticData = transitionStaticData;
      this.npcSearcher = npcSearcher;
      this.stealPreparing = stealPreparing;

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
        animator, stealPreparing, npcSearcher);
      SaveState(state);
    }
    
    private void CreateInteractionState()
    {
      HeroInteractionState state = new HeroInteractionState(hero, animator, npcSearcher, stealPreparing);
      SaveState(state);
    }

    private void SaveState(BaseStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}