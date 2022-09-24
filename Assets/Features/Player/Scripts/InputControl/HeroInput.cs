using Features.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using Zenject;

namespace Features.Player.Scripts.InputControl
{
  public class HeroInput : MonoBehaviour
  {
    private readonly IInputCommand[] readedInputs = new IInputCommand[20];
    
    private int inputIndex = 0;

    private IInputService inputService;

    public ReadOnlyArray<IInputCommand> Commands => readedInputs;
    public int CommandsCount => inputIndex;

    [Inject]
    public void Construct(IInputService inputService)
    {
      this.inputService = inputService;
      this.inputService.Enable();
    }

    public void Cleanup()
    {
      inputService.Disable();
      inputService.Cleanup();
    }
    public void LockInput() { }
    public void UnlockInput() { }

    public void ReadInput() => 
      inputService.ReadInput(readedInputs, ref inputIndex);

    public void ClearInput() => 
      inputIndex = 0;
  }
}