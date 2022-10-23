using System;
using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.NPCStates;
using Features.Customers.Scripts.Timing;
using Features.Player.Scripts.HeroMachine;
using UniRx;
using UnityEngine;

namespace Features.Customers.Scripts.Base
{
  public class NPCStateMachineObserver : BaseStateMachineObserver
  {
    [SerializeField] private SimpleAnimator animator;

    private UpdatableStateMachine stateMachine;
    private NPCStatesContainer statesContainer;

    private readonly CompositeDisposable disposable = new CompositeDisposable();

    public event Action Leaved;
    public event Action Robbed;

    public void Construct(NPCStatesContainer container, NPCAlertness alertness,
      NPCExistTimeObserver existTimeObserver)
    {
      statesContainer = container; 
      stateMachine = new UpdatableStateMachine();

      alertness.IsWary.Subscribe(OnWary).AddTo(disposable);
      existTimeObserver.IsNeedToExit.Subscribe(OnTimeOut).AddTo(disposable);
    }

    public virtual void Subscribe()
    {
      base.Subscribe();
      animator.Triggered += OnAnimationTriggered;
    }

    public override void Cleanup()
    {
      base.Cleanup();
      animator.Triggered -= OnAnimationTriggered;
      disposable.Clear();
    }

    public override void CreateStates() => 
      statesContainer.CreateStates();

    public override void SetDefaultState() => 
      stateMachine.SetState(statesContainer.GetState<NPCIdleState>());

    public void UpdateState(float deltaTime) => 
      stateMachine.UpdateState(deltaTime);

    public void ChangeState<TState>() where TState : NPCStateMachineState => 
      stateMachine.ChangeState(statesContainer.GetState<TState>());

    public void ChangeState<TState>(TState state) where TState : NPCStateMachineState => 
      stateMachine.ChangeState(state);

    public TState State<TState>() where TState : NPCStateMachineState => 
      statesContainer.GetState<TState>();

    public void GoToStartPoint(Vector3 target)
    {
      NPCMoveState state = State<NPCMoveState>();
      state.SaveFinishPosition(target);
      ChangeState(state);
    }

    public void SetRobbedState()
    {
      ChangeState<NPCRobbedState>();
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