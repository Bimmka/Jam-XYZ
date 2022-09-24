using Features.Player.Scripts.HeroMachine.States;
using Features.Services.Input;
using Features.StateMachines;
using UnityEngine.InputSystem.Utilities;

namespace Features.Player.Scripts.HeroMachine
{
  public class HeroStateMachine : BaseStateMachine
  {
    public void UpdateState(ReadOnlyArray<IInputCommand> commands, int commandsCount, float deltaTime)
    {
      ((HeroStateMachineState) State).Update(commands, commandsCount, deltaTime);
    }
  }
}