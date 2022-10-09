using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.Player.Scripts.Gold;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.Steal;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.StealWindow.Scripts;

namespace Features.Player.Scripts.HeroMachine.States.Interaction
{
  public class HeroInteractionState : HeroStateMachineState
  {
    private readonly HeroNPCSearcher npcSearcher;
    private readonly HeroStealPreparing stealPreparing;
    private readonly IWindowsService windowsService;
    private readonly HeroGold heroGold;

    private NPCAlertnessObserver npc;

    public HeroInteractionState(HeroStateMachineObserver hero, ChangeableParametersAnimator animator, HeroNPCSearcher npcSearcher, 
      HeroStealPreparing stealPreparing, IWindowsService windowsService, HeroGold heroGold) : base(hero, animator)
    {
      this.npcSearcher = npcSearcher;
      this.stealPreparing = stealPreparing;
      this.windowsService = windowsService;
      this.heroGold = heroGold;
    }

    public override void Enter()
    {
      base.Enter();
      InitializeStealWindow();
      stealPreparing.ResetPreparing();
      npc.GetComponent<NPCStateMachineObserver>().SetRobbedState();
      heroGold.Add(10);
      
      ChangeState<HeroIdleState>();
    }

    public override void Exit()
    {
      base.Exit();
      npcSearcher.StartSearch();
    }

    public void SaveStolenNPC(NPCAlertnessObserver stolenNPC)
    {
      npc = stolenNPC;
    }

    private void InitializeStealWindow()
    {
      windowsService.Open(WindowId.StealWindow);
      UIStealWindow stealWindow = (UIStealWindow) windowsService.Window(WindowId.StealWindow);

      stealWindow.Initialize(stealPreparing.PrepareAmount.Value);
    }
  }
}