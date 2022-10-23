namespace Features.Customers.Scripts.NPCStates
{
  public interface IUpdatableState
  {
    void UpdateState(in float deltaTime);
  }
}