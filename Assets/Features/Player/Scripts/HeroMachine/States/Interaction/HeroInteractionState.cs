using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.Player.Scripts.Gold;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.Steal;
using Features.Services.Input;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.StaticData.StealItems;
using Features.UI.Windows.StealWindow.Scripts;
using UniRx;
using UnityEngine.InputSystem.Utilities;

namespace Features.Player.Scripts.HeroMachine.States.Interaction
{
  public class HeroInteractionState : HeroStateMachineState
  {
    private readonly HeroNPCSearcher npcSearcher;
    private readonly IWindowsService windowsService;
    private readonly HeroGold heroGold;
    private readonly StealItemCostStaticData costStaticData;

    private NPCAlertnessObserver npc;

    private UIStealWindow stealWindow;

    private bool isCatching;

    private float stealTime;

    public HeroInteractionState(HeroStateMachineObserver hero, ChangeableParametersAnimator animator, HeroNPCSearcher npcSearcher, 
     IWindowsService windowsService, HeroGold heroGold, StealItemCostStaticData costStaticData) : base(hero, animator)
    {
      this.npcSearcher = npcSearcher;
      this.windowsService = windowsService;
      this.heroGold = heroGold;
      this.costStaticData = costStaticData;
    }

    public override void Enter()
    {
      base.Enter();
      InitializeStealWindow();
    }
    
    public override void Update(ReadOnlyArray<IInputCommand> commands, int commandsCount, float deltaTime)
    {
      base.Update(commands, commandsCount, deltaTime);
      
      if (commandsCount == 0)
        return;
      
      for (int i = 0; i < commandsCount; i++)
      {
        ApplyCommand(commands[i], deltaTime);
      }
    }

    public override void Exit()
    {
      base.Exit();
      isCatching = false;
      npcSearcher.StartSearch();
    }

    public void Initialize(NPCAlertnessObserver stolenNPC, float stealTime)
    {
      this.stealTime = stealTime;
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
      stealWindow.Initialize(stealTime, OnHitGold, OnHitRing, OnMiss, OnTimeOut);
    }

    private void OnHitRing()
    {
      npc.ChangeStealableState(false);
      npc.AddAttention(costStaticData.Cost(StealItemType.Ring));
      if (npc.IsWary == false)
        RobeNPC();
      
      ChangeState<HeroIdleState>();
    }

    private void OnMiss()
    {
      npc.ChangeStealableState(false);
      npc.Warn();
      ChangeState<HeroIdleState>();
    }

    private void OnHitGold(StealItemType type)
    {
      RobeNPC();
      AddGold(costStaticData.Cost(type));
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