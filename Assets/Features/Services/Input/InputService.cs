using System;
using InputControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Services.Input
{
  public class InputService : IInputService
  {
    private readonly HeroControl input;
    private readonly InputCommandVector inputVector;
    private readonly InputCommandsContainer commandContainer;

    public event Action SpecialKeyClicked;

    public InputService(HeroControl inputMap)
    {
      input = inputMap;
      input.Hero.SpecialAction.performed += OnSpecialActionClicked;
      commandContainer = new InputCommandsContainer();
    }

    public void Cleanup()
    {
      input.Hero.SpecialAction.performed -= OnSpecialActionClicked;
    }

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

      if (input.Hero.SpecialAction.WasPerformedThisFrame() && IsFitInLength(readedInputs, inputIndex))
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

    private void OnSpecialActionClicked(InputAction.CallbackContext context)
    {
      SpecialKeyClicked?.Invoke();
    }
  }
}