using Features.Animation;
using Features.Player.Scripts.HeroMachine.States.Interaction;
using Features.Player.Scripts.Steal;
using Features.Services.Input;
using Features.StaticData.Hero.AnimationTransitions;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Features.Player.Scripts.HeroMachine.States.Base
{
  public class HeroIdleState : HeroStateMachineState
  {
    private readonly FloatAnimationTransitionStaticData transitionsData;
    private readonly HeroNPCSearcher npcSearcher;

    public HeroIdleState(HeroStateMachineObserver hero, FloatAnimationTransitionStaticData transitionsData, 
      ChangeableParametersAnimator animator, HeroNPCSearcher npcSearcher) : base(hero, animator)
    {
      this.transitionsData = transitionsData;
      this.npcSearcher = npcSearcher;
    }

    public override void Enter()
    {
      base.Enter();
      SetBool(transitionsData.HashedName, true);
      ChangeParameter(transitionsData.FirstTransition.HashedName, transitionsData.FirstTransition.EndValue, transitionsData.FirstTransition.Duration);
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
      SetBool(transitionsData.HashedName, false);
    }

    protected override void ApplyMoveCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyMoveCommand(command, deltaTime);
      
      if (command.Vector == Vector2.zero) 
        return;
      
      ChangeState<HeroMoveState>();
    }

    protected override void ApplyInteractCommand(InputCommandBool command, float deltaTime)
    {
      base.ApplyInteractCommand(command, deltaTime);
      
      if (npcSearcher.IsFoundNPC())
        ChangeState<HeroInteractionPrepareState>();
      
    }
  }
}