using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Player.Scripts.HeroMachine.States.Base;
using Features.Player.Scripts.Steal;
using Features.Services.Input;
using Features.StaticData.Hero.AnimationTransitions;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Features.Player.Scripts.HeroMachine.States.Interaction
{
  public class HeroInteractionPrepareState : HeroStateMachineState
  {
    private readonly BaseAnimationTransitionStaticData animationTransition;
    private readonly HeroStealPreparing stealPreparing;
    private readonly HeroNPCSearcher npcSearcher;

    private bool isCanBeInterrupted;
    
    private CompositeDisposable disposable = new CompositeDisposable();

    private NPCAlertnessObserver stolenNPC;
    public HeroInteractionPrepareState(HeroStateMachineObserver hero,
      BaseAnimationTransitionStaticData animationTransition, ChangeableParametersAnimator animator,
      HeroStealPreparing stealPreparing,
      HeroNPCSearcher npcSearcher)
      : base(hero, animator)
    {
      this.animationTransition = animationTransition;
      this.stealPreparing = stealPreparing;
      this.npcSearcher = npcSearcher;
      stealPreparing.IsFullPrepared.Subscribe(OnFullPrepare).AddTo(disposable);
    }

    public override void Enter()
    {
      base.Enter();
      isCanBeInterrupted = false;
      stealPreparing.StartStealPrepare();
      stolenNPC = npcSearcher.LastHitNPC;
      npcSearcher.DisableSearch();
      stolenNPC.StopRelaxation();
      stolenNPC.StartPayingAttention();
      stolenNPC.ShowAlertnessTip();

      SetBool(animationTransition.HashedName, true);
    }

    public override void Update(ReadOnlyArray<IInputCommand> commands, int commandsCount, float deltaTime)
    {
      base.Update(commands, commandsCount, deltaTime);
      
      if (commandsCount == 0 || isCanBeInterrupted == false)
        return;
      
      for (int i = 0; i < commandsCount; i++)
      {
        ApplyCommand(commands[i], deltaTime);
      }
    }

    public override void Exit()
    {
      base.Exit();
      stealPreparing.ResetPreparing();
      SetBool(animationTransition.HashedName, false);
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      isCanBeInterrupted = true;
    }

    public void StopPrepareByAlarm()
    {
      stealPreparing.ResetPreparing();
      ChangeState<HeroIdleState>();
      npcSearcher.StartSearch();
      stolenNPC.ShowWaryTip();
    }

    protected override void ApplyMoveCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyMoveCommand(command, deltaTime);
      
      if (command.Vector == Vector2.zero)
        return;
      
      ChangeState<HeroMoveState>();
      
      if (stolenNPC.IsWary == false)
      {
        stolenNPC.StopPayingAttention();
        stolenNPC.StartRelaxation();
        stolenNPC.HideAlertnessTip();
      }
      npcSearcher.StartSearch();
    }

    protected override void ApplyInteractCommand(InputCommandBool command, float deltaTime)
    {
      base.ApplyInteractCommand(command, deltaTime);
      StartStealGame();
    }

    private void OnFullPrepare(bool isFull)
    {
      if (isFull)
        StartStealGame();
    }

    private void StartStealGame()
    {
      stolenNPC.StopPayingAttention();
      stolenNPC.HideAlertnessTip();
      HeroInteractionState state = hero.State<HeroInteractionState>();
      state.SaveStolenNPC(stolenNPC);
      ChangeState<HeroInteractionState>();
    }
  }
}