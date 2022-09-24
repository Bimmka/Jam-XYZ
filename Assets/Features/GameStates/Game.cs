using Features.SceneLoading.Scripts;
using Features.Services.Coroutine;

namespace Features.GameStates
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner,curtain));
    }

    public void Cleanup()
    {
      StateMachine.Cleanup();
    }
  }
}