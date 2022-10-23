using System;
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
    
    private PoliceSettings settings;

    public void Construct(PoliceSettings settings, PolicePathObserver pathObserver)
    {
      this.settings = settings;
      PoliceHeroSearcher searcher = new PoliceHeroSearcher(raycastOrigin, settings);
      searcher.StartSearch();
      PoliceStatesContainer statesContainer =
        new PoliceStatesContainer(stateMachine, animator, searcher, aiPath, destinationSetter, pathObserver, settings);
      
      stateMachine.Construct(statesContainer);
      stateMachine.Subscribe();
      stateMachine.CreateStates();
    }

    private void Start()
    {
      stateMachine.SetDefaultState();
    }

    private void OnDrawGizmos()
    {
      Gizmos.DrawWireSphere(transform.position, settings.ArrestDistance);
    }
  }
}
