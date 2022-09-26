using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.NPCStates;
using UniRx;
using UnityEngine;

namespace Features.Customers.Scripts.Base
{
  public class NPCStateMachineObserver : MonoBehaviour
  {
    [SerializeField] private SimpleAnimator animator;

    private NPCStateMachine stateMachine;
    private NPCStatesContainer statesContainer;
    private NPCAlertness alertness;
    
    private CompositeDisposable disposable = new CompositeDisposable();

    public void Construct(NPCStatesContainer container, NPCAlertness alertness)
    {
      this.alertness = alertness;
      statesContainer = container; 
      stateMachine = new NPCStateMachine();

      alertness.IsWary.Subscribe(onNext => OnWary(onNext)).AddTo(disposable);
    }

    public void Subscribe() => 
      animator.Triggered += OnAnimationTriggered;

    public void Cleanup()
    {
      animator.Triggered -= OnAnimationTriggered;
      disposable.Clear();
    }

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

    private void OnWary(in bool isWary)
    {
      if (isWary)
        ChangeState<NPCWarningState>();
    }
  }
}