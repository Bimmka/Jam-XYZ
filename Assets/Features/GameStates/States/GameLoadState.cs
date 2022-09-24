using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly GameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;

    public GameLoadState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IWindowsService windowsService)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
    }

    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }

    private void OnLoad()
    {
      CreateHUD();
      gameStateMachine.Enter<GameLoopState>();
    }
    
    private void CreateHUD() => 
      windowsService.Open(WindowId.LevelMenu);
    
  }
}