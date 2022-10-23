using Features.Customers.Scripts.NPCStates;
using Features.Player.Scripts.HeroMachine;
using Features.Police.Scripts.PoliceStates.States;

namespace Features.Police.Scripts.PoliceStates
{
  public class PoliceStateMachineObserver : BaseStateMachineObserver
  {
    private PoliceStatesContainer statesContainer;

    private UpdatableStateMachine stateMachine;

    public void Construct(PoliceStatesContainer container)
    {
      statesContainer = container;
    }

    public override void CreateStates()
    {
      statesContainer.CreateStates();
    }

    public override void SetDefaultState()
    {
      
    }

    public void ChangeState<TState>() where TState : PoliceStateMachineState => 
      stateMachine.ChangeState(statesContainer.GetState<TState>());
    
    public void ChangeState<TState>(TState state) where TState : PoliceStateMachineState => 
      stateMachine.ChangeState(state);
    
    public TState State<TState>() where TState : PoliceStateMachineState => 
      statesContainer.GetState<TState>();
  }
}