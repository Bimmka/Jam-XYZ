using Features.SceneLoading.Scripts;
using Features.Services.Coroutine;

namespace Features.GameStates
{
  public class Game
  {
    public readonly IGameStateMachine StateMachine;

    public Game(IGameStateMachine gameStateMachine)
    {
      StateMachine = gameStateMachine;
    }

    public void Cleanup()
    {
      
    }
  }
}