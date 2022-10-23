using Features.Animation;
using UnityEngine;

namespace Features.Police.Scripts.PoliceStates.States
{
  public class PoliceArrestingState : PoliceStateMachineState
  {
    public PoliceArrestingState(PoliceStateMachineObserver police, SimpleAnimator animator, string animationName) : base(police, animator, animationName)
    {
    }

    public override void Enter()
    {
      base.Enter();
      ArrestPlayer();
    }

    private void ArrestPlayer() => 
      Debug.Log("Player Arrested");
  }
}