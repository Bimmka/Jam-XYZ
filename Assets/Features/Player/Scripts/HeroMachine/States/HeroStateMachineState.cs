using Features.Animation;
using Features.Services.Input;
using Features.StateMachines;
using UnityEngine.InputSystem.Utilities;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroStateMachineState : BaseStateMachineState
  {
    protected readonly HeroStateMachineObserver hero;
    protected readonly ChangeableParametersAnimator animator;

    public HeroStateMachineState(HeroStateMachineObserver hero, ChangeableParametersAnimator animator)
    {
      this.hero = hero;
      this.animator = animator;
    }
    
    public virtual void Update(ReadOnlyArray<IInputCommand> commands, int commandsCount, float deltaTime) {}

    protected void ChangeState<TState>() where TState : HeroStateMachineState => 
      hero.ChangeState<TState>();
      
    protected void ApplyCommand(IInputCommand command, float deltaTime)
    {
      
      switch (command.Type)
      {
        case InputCommandType.Move:
          ApplyMoveCommand((InputCommandVector) command, deltaTime);
          break;
        case InputCommandType.Interact:
          ApplyInteractCommand((InputCommandBool) command, deltaTime);
          break;
        case InputCommandType.SpecialAction:
          ApplySpecialCommand((InputCommandBool) command, deltaTime);
          break;
      }
    }

    protected virtual void ApplyMoveCommand(InputCommandVector command, float deltaTime) { }

    protected virtual void ApplyInteractCommand(InputCommandBool command, float deltaTime) { }
    

    protected virtual void ApplySpecialCommand(InputCommandBool command, float deltaTime) { }

    protected void SetBool(int hashName, bool isEnable) => 
      animator.SetBool(hashName, isEnable);

    protected void ChangeParameter(int floatHashName, float endValue, float duration) => 
      animator.ChangeParameter(floatHashName, endValue, duration);
  }
}