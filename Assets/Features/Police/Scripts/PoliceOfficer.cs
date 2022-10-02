using UnityEngine;

namespace Features.Police.Scripts
{
  [RequireComponent(typeof(PoliceStateMachineObserver))]
  public class PoliceOfficer : MonoBehaviour
  {
    [SerializeField] private PoliceStateMachineObserver stateMachine;

    public void Construct(PoliceStatesContainer container)
    {
      stateMachine.Construct(container);
      stateMachine.Subscribe();
      stateMachine.CreateStates();
      stateMachine.SetDefaultState();
    }
  }
}
