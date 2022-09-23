using System;
using System.Collections.Generic;
using Features.Animation;
using Features.Hero.Scripts.HeroMachine.States.Base;
using Features.Hero.Scripts.Move;
using Features.StateMachines;
using Features.StaticData.Hero.AnimationTransitions;

namespace Features.Hero.Scripts.HeroMachine.States
{
  public class HeroStatesContainer
  {
    private readonly HeroStateMachineObserver hero;
    private readonly HeroMove move;
    private readonly ChangeableParametersAnimator animator;
    private readonly HeroAnimationsTransitionStaticData transitionStaticData;

    private readonly Dictionary<Type, BaseStateMachineState> states;
    public HeroStatesContainer(HeroStateMachineObserver hero, HeroMove move, ChangeableParametersAnimator animator,
      HeroAnimationsTransitionStaticData transitionStaticData)
    {
      this.hero = hero;
      this.move = move;
      this.animator = animator;
      this.transitionStaticData = transitionStaticData;
    
      states = new Dictionary<Type, BaseStateMachineState>(20);
    }

    public void CreateStates()
    {
      CreateIdleState();
      CreateRunState();
    }

    public TState GetState<TState>() where TState : BaseStateMachineState
    {
      if (states.ContainsKey(typeof(TState)))
        return (TState)states[typeof(TState)];

      throw new ArgumentNullException();
    }

    private void CreateIdleState()
    {
      HeroIdleState state = new HeroIdleState(hero, (FloatAnimationTransitionStaticData) transitionStaticData.Transitions["Idle"], animator);
      SaveState(state);
    }

    private void CreateRunState()
    {
      HeroMoveState state = new HeroMoveState(hero,move, (FloatAnimationTransitionStaticData)transitionStaticData.Transitions["Run"], animator);
      SaveState(state);
    }

    private void SaveState(BaseStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}