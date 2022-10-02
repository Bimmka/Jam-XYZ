using UnityEngine;

namespace Features.Player.Scripts.HeroMachine
{
  public abstract class BaseStateMachineObserver : MonoBehaviour
  {
    public abstract void CreateStates();
    public abstract void SetDefaultState();
    public virtual void Subscribe() {}

    public virtual void Cleanup() {}
  }
}