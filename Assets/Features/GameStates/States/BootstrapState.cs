using Features.GameStates.States.Interfaces;

namespace Features.GameStates.States
{
  public class BootstrapState : IState
  {
    private readonly IGameStateMachine gameStateMachine;

    public BootstrapState(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
      gameStateMachine.Add(this);
    }

    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }

    private void RegisterServices()
    {
    }
    
  }
}