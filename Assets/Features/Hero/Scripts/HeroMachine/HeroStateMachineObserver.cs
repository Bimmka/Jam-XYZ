using Features.Animation;
using Features.Hero.Scripts.HeroMachine.States;
using Features.Hero.Scripts.HeroMachine.States.Base;
using Features.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Features.Hero.Scripts.HeroMachine
{
  public class HeroStateMachineObserver : MonoBehaviour
  {
    [SerializeField] private ChangeableParametersAnimator animator;
    
    private HeroStateMachine stateMachine;
    private HeroStatesContainer statesContainer;

    public void Construct(HeroStatesContainer container)
    {
      statesContainer = container; 
      stateMachine = new HeroStateMachine();
    }

    public void Subscribe() => 
      animator.Triggered += OnAnimationTriggered;

    public void Cleanup() => 
      animator.Triggered -= OnAnimationTriggered;

    public void CreateStates() => 
      statesContainer.CreateStates();

    public void SetDefaultState() => 
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