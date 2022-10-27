using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Zenject;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;

    [Inject]
    public GameLoadState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IWindowsService windowsService)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
      gameStateMachine.Register(this);
    }

    public void Enter()
    {
      sceneLoader.Load(GameConstants.GameSceneName, OnLoad);
    }

    public void Exit()
    {
      
    }

    private void OnLoad()
    {
      CreateHUD();
      CreateLevelPrepareWindow();
      gameStateMachine.Enter<GameLoopState>();
    }
    
    private void CreateHUD() => 
      windowsService.Open(WindowId.HUD);

    private void CreateLevelPrepareWindow() => 
      windowsService.Open(WindowId.LevelPrepare);
  }
}