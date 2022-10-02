using Features.Animation;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.HeroMachine.States.Interaction;
using Features.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Features.Player.Scripts.HeroMachine
{
  public class HeroStateMachineObserver : BaseStateMachineObserver
  {
    [SerializeField] private ChangeableParametersAnimator animator;
    
    private HeroStateMachine stateMachine;
    private HeroStatesContainer statesContainer;

    public void Construct(HeroStatesContainer container)
    {
      statesContainer = container; 
      stateMachine = new HeroStateMachine();
    }

    public override void Subscribe()
    {
      base.Subscribe();
      animator.Triggered += OnAnimationTriggered;
    }

    public override void Cleanup()
    {
      base.Cleanup();
      animator.Triggered -= OnAnimationTriggered;
      State<HeroInteractionPrepareState>().Cleanup();
    }

    public override void CreateStates() => 
      statesContainer.CreateStates();

    public override void SetDefaultState() => 
      stateMachine.SetState(statesContainer.GetState<HeroIdleState>());

    public void UpdateState(ReadOnlyArray<IInputCommand> commands, int commandsCount, float deltaTime) => 
      stateMachine.UpdateState(commands, commandsCount, deltaTime);

    public void ChangeState<TState>() where TState : HeroStateMachineState => 
      stateMachine.ChangeState(statesContainer.GetState<TState>());
    
    public void ChangeState<TState>(TState state) where TState : HeroStateMachineState => 
      stateMachine.ChangeState(state);
    
    public TState State<TState>() where TState : HeroStateMachineState => 
      statesContainer.GetState<TState>();

    private void OnAnimationTriggered() => 
      stateMachine.State.TriggerAnimation();
  }
}