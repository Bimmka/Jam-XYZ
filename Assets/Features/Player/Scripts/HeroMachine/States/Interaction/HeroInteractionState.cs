using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.Player.Scripts.Gold;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.Steal;
using Features.Services.Input;
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

    private UIStealWindow stealWindow;

    private bool isCatching;

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
    }

    public override void Exit()
    {
      base.Exit();
      isCatching = false;
      npcSearcher.StartSearch();
    }

    public void SaveStolenNPC(NPCAlertnessObserver stolenNPC)
    {
      npc = stolenNPC;
    }

    protected override void ApplySpecialCommand(InputCommandBool command, float deltaTime)
    {
      base.ApplySpecialCommand(command, deltaTime);
      if (isCatching == false)
      {
        stealWindow.Catch();
        isCatching = true;
      }
    }

    private void InitializeStealWindow()
    {
      windowsService.Open(WindowId.StealWindow);
      stealWindow = (UIStealWindow) windowsService.Window(WindowId.StealWindow);
      stealWindow.Initialize(stealPreparing.PrepareAmount.Value, OnHitGold, OnHitRing, OnMiss, OnTimeOut);
    }

    private void OnHitRing()
    {
      npc.ChangeStealableState(false);
      npc.AddAttention(10);
      ChangeState<HeroIdleState>();
    }

    private void OnMiss()
    {
      npc.ChangeStealableState(false);
      npc.Warn();
      ChangeState<HeroIdleState>();
    }

    private void OnHitGold(int count)
    {
      RobeNPC();
      AddGold(count);
      ChangeState<HeroIdleState>();
    }

    private void OnTimeOut()
    {
      RobeNPC();
      ChangeState<HeroIdleState>();
    }

    private void RobeNPC() => 
      npc.GetComponent<NPCStateMachineObserver>().SetRobbedState();

    private void AddGold(in int count) => 
      heroGold.Add(count);
  }
}