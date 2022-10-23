using Features.Animation;
using Features.Police.Data;
using Features.Police.Scripts.Path;
using Features.Police.Scripts.Searching;
using Pathfinding;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates
{
  [RequireComponent(typeof(PoliceStateMachineObserver))]
  public class PoliceOfficer : MonoBehaviour
  {
    [SerializeField] private PoliceStateMachineObserver stateMachine;
    [SerializeField] private SimpleAnimator animator;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private AIPath aiPath;
    [SerializeField] private AIDestinationSetter destinationSetter;

    public void Construct(PoliceSettings settings, PolicePathObserver pathObserver)
    {
      PoliceHeroSearcher searcher = new PoliceHeroSearcher(raycastOrigin, settings);
      PoliceStatesContainer statesContainer =
        new PoliceStatesContainer(stateMachine, animator, searcher, aiPath, destinationSetter,pathObserver);
      
      stateMachine.Construct(statesContainer);
      stateMachine.Subscribe();
      stateMachine.CreateStates();
    }

    private void Start()
    {
      stateMachine.SetDefaultState();
    }
  }
}
