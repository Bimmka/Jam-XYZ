using System;
using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.NPCStates;
using Features.Customers.Scripts.Timing;
using UniRx;
using UnityEngine;

namespace Features.Customers.Scripts.Base
{
  public class NPCStateMachineObserver : MonoBehaviour
  {
    [SerializeField] private SimpleAnimator animator;

    private NPCStateMachine stateMachine;
    private NPCStatesContainer statesContainer;

    private readonly CompositeDisposable disposable = new CompositeDisposable();

    public event Action Leaved;
    public event Action Robbed;

    public void Construct(NPCStatesContainer container, NPCAlertness alertness,
      NPCExistTimeObserver existTimeObserver)
    {
      statesContainer = container; 
      stateMachine = new NPCStateMachine();

      alertness.IsWary.Subscribe(OnWary).AddTo(disposable);
      existTimeObserver.IsNeedToExit.Subscribe(OnTimeOut).AddTo(disposable);
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

    public void NotifyAboutLeave() => 
      Leaved?.Invoke();

    public void NotifyAboutRobbed() => 
      Robbed?.Invoke();

    private void OnAnimationTriggered() => 
      stateMachine.State.TriggerAnimation();

    private void OnWary(bool isWary)
    {
      if (isWary)
        ChangeState<NPCWarningState>();
    }

    private void OnTimeOut(bool isTimeOut)
    {
      if (isTimeOut)
        ChangeState<NPCLeaveState>();
    }
  }
}