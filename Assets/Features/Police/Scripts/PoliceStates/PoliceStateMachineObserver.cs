using Features.Animation;
using Features.Customers.Scripts.NPCStates;
using Features.Player.Scripts.HeroMachine;
using Features.Police.Scripts.PoliceStates.States;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates
{
  public class PoliceStateMachineObserver : BaseStateMachineObserver
  {
    [SerializeField] private SimpleAnimator animator;
    
    private PoliceStatesContainer statesContainer;

    private UpdatableStateMachine stateMachine;
    public bool IsFollowing => stateMachine.State != null && stateMachine.State.GetType() == typeof(PoliceFollowState);

    public void Construct(PoliceStatesContainer container)
    {
      statesContainer = container;
      stateMachine = new UpdatableStateMachine();
    }

    private void Update() => 
      stateMachine.UpdateState(Time.deltaTime);

    public override void Subscribe()
    {
      base.Subscribe();
      animator.Triggered += TriggerAnimation;
    }

    public override void Cleanup()
    {
      base.Cleanup();
      animator.Triggered -= TriggerAnimation;
    }

    public override void CreateStates() => 
      statesContainer.CreateStates();

    public override void SetDefaultState() => 
      stateMachine.SetState(State<PoliceIdleState>());

    public void GoToRobbedGuest(Vector3 position)
    {
      PoliceWarnedState warnedState = State<PoliceWarnedState>();
      warnedState.SaveInvokePosition(position);
      ChangeState(warnedState);
    }

    public void ChangeState<TState>() where TState : PoliceStateMachineState => 
      stateMachine.ChangeState(statesContainer.GetState<TState>());

    public void ChangeState<TState>(TState state) where TState : PoliceStateMachineState => 
      stateMachine.ChangeState(state);

    public TState State<TState>() where TState : PoliceStateMachineState => 
      statesContainer.GetState<TState>();

    public void StopFollow() => 
      ((PoliceFollowState)stateMachine.State).LoseFollow();

    private void TriggerAnimation() => 
      stateMachine.State.TriggerAnimation();
  }
}