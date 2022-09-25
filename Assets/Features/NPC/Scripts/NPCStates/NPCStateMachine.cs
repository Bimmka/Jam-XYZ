using Features.StateMachines;

namespace Features.NPC.Scripts.NPCStates
{
  public class NPCStateMachine : BaseStateMachine
  {
    public void UpdateState(float deltaTime) => 
      ((NPCStateMachineState)State).UpdateState(deltaTime);
  }
}