using Features.Customers.Scripts.Factory;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Zenject;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly GameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;

    [Inject]
    public GameLoadState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, NPCFactory npcFactory)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      
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