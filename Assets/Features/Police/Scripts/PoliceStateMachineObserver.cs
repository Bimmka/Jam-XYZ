using Features.Player.Scripts.HeroMachine;
using UnityEngine;

namespace Features.Police.Scripts
{
  public class PoliceStateMachineObserver : BaseStateMachineObserver
  {
    private PoliceStatesContainer container;

    public void Construct(PoliceStatesContainer container)
    {
      this.container = container;
    }

    public override void CreateStates()
    {
      container.CreateStates();
    }

    public override void SetDefaultState()
    {
      
    }
  }
}