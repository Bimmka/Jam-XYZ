using Features.Animation;
using Features.Customers.Scripts.NPCStates;
using UnityEngine;

namespace Features.Customers.Scripts.Base
{
  public class NPCStateMachineObserver : MonoBehaviour
  {
    [SerializeField] private SimpleAnimator animator;
    
    private NPCStateMachine stateMachine;
    private NPCStatesContainer statesContainer;

    public void Construct(NPCStatesContainer container)
    {
      statesContainer = container; 
      stateMachine = new NPCStateMachine();
    }

    public void Subscribe() => 
      animator.Triggered += OnAnimationTriggered;

    public void Cleanup() => 
      animator.Triggered -= OnAnimationTriggered;

    public void CreateStates() => 
      statesContainer.CreateStates();

    public void SetDefaultState() => 
      stateMachine.SetState(statesContainer.GetState<NPCIdleState>());

    public void UpdateState(float deltaTime) => 
      stateMachine.UpdateState(deltaTime);

    public void ChangeState<TState>() where TState : NPCStateMachineState => 
      stateMachine.ChangeState(statesContainer.GetState<TState>());
    
    public void ChangeState<TState>(TState state) where TState : NPCStateMachineState => 
      stateMachine.ChangeState(state);
    
    public TState State<TState>() where TState : NPCStateMachineState => 
      statesContainer.GetState<TState>();

    public void GoToStartPoint(Transform target)
    {
      NPCMoveState state = State<NPCMoveState>();
      state.SaveFinishPosition(target);
      ChangeState(state);
    }

    private void OnAnimationTriggered() => 
      stateMachine.State.TriggerAnimation();
  }
}