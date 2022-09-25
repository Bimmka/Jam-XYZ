using Features.StateMachines;

namespace Features.Customers.Scripts.NPCStates
{
  public class NPCStateMachine : BaseStateMachine
  {
    public void UpdateState(float deltaTime) => 
      ((NPCStateMachineState)State).UpdateState(deltaTime);
  }
}