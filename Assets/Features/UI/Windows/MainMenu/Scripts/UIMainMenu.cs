using Features.GameStates;
using Features.GameStates.States;
using Features.UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.MainMenu.Scripts
{
  [RequireComponent(typeof(UIMainMenuView))]
  public class UIMainMenu : BaseWindow
  {
    [SerializeField] private UIMainMenuView view;
    [SerializeField] private Button startGameButton;

    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      startGameButton.onClick.AddListener(TryJoinLobby);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      startGameButton.onClick.RemoveListener(TryJoinLobby);
    }

    private void TryJoinLobby() => 
      gameStateMachine.Enter<GameLoadState>();
  }
}