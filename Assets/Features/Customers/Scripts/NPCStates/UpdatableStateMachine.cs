using Features.StateMachines;

namespace Features.Customers.Scripts.NPCStates
{
  public class UpdatableStateMachine : BaseStateMachine
  {
    public void UpdateState(float deltaTime) => 
      ((IUpdatableState)State).UpdateState(deltaTime);
  }
}