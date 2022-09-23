using InputControl;
using UnityEngine;

namespace Features.Services.Input
{
  public class InputService : IInputService
  {
    private readonly HeroControl input;
    private readonly InputCommandVector inputVector;
    private readonly InputCommandsContainer commandContainer;

    public InputService(HeroControl inputMap)
    {
      input = inputMap;
      commandContainer = new InputCommandsContainer();
    }

    public void Cleanup() { }

    public void Enable() => 
      input.Enable();

    public void Disable() => 
      input.Disable();

    public void ReadInput(IInputCommand[] readedInputs, ref int inputIndex)
    {
      IInputCommand command;

      if (IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.Move);
        ((InputCommandVector)command).SetValue(input.Hero.Move.ReadValue<Vector2>());

        AddCommand(readedInputs, ref inputIndex, command);
      }

      if (input.Hero.Interuct.WasPerformedThisFrame() && IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.Interact);
        
        ((InputCommandBool)command).SetValue(true);

        AddCommand(readedInputs, ref inputIndex, command);
      }

      if (input.Hero.CalmLibrary.WasPerformedThisFrame() && IsFitInLength(readedInputs, inputIndex))
      {
        command = commandContainer.Command(InputCommandType.SpecialAction);
        
        ((InputCommandBool)command).SetValue(true);

        AddCommand(readedInputs, ref inputIndex, command);
      }
      
    }
    
    private bool IsFitInLength(IInputCommand[] readedInputs, int inputIndex)
    {
      return inputIndex < readedInputs.Length;
    }

    private void AddCommand(IInputCommand[] readedInputs, ref int index, IInputCommand command)
    {
      readedInputs[index] = command;
      index++;
    }
  }
}