using Features.Player.Scripts.HeroMachine;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.InputControl;
using UnityEngine;

namespace Features.Player.Scripts.Base
{
  [RequireComponent(typeof(HeroInput))]
  [RequireComponent(typeof(HeroStateMachineObserver))]
  [RequireComponent(typeof(Rigidbody2D))]
  public class Hero : MonoBehaviour
  {
    [SerializeField] private HeroInput input;
    [SerializeField] private HeroStateMachineObserver stateMachine;
    [SerializeField] private Rigidbody2D heroBody;

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