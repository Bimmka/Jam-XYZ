namespace Features.StateMachines
{
  public abstract class BaseStateMachineState
  {
    protected int animationName;

    //public abstract bool IsCanBeInterrupted();

    public virtual void Enter() {}

    public virtual void Exit() { }

    public virtual void TriggerAnimation() { }
  }
}