namespace Features.Services.Input
{
  public interface IInputService : ICleanupService
  {
    void Enable();
    void Disable();
    void ReadInput(IInputCommand[] readedInputs, ref int inputIndex);
  }
}