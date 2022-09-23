using Features.Hero.Scripts.HeroMachine;
using Features.Hero.Scripts.HeroMachine.States;
using Features.Hero.Scripts.InputControl;
using UnityEngine;

namespace Features.Hero.Scripts.Base
{
  [RequireComponent(typeof(HeroInput))]
  [RequireComponent(typeof(HeroStateMachineObserver))]
  [RequireComponent(typeof(CharacterController))]
  public class Hero : MonoBehaviour
  {
    [SerializeField] private HeroInput input;
    [SerializeField] private HeroStateMachineObserver stateMachine;
    [SerializeField] private CharacterController characterController;

    public void Construct(HeroStatesContainer container)
    {
      stateMachine.Construct(container);
      stateMachine.Subscribe();
      stateMachine.CreateStates();
      stateMachine.SetDefaultState();
    }

    private void OnDestroy()
    {
      stateMachine.Cleanup();
      input.Cleanup();
    }

    private void Update()
    {
      input.ReadInput();
      stateMachine.UpdateState(input.Commands, input.CommandsCount, Time.deltaTime);
      input.ClearInput();
    }
  }
}