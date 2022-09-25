using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.StateMachines;

namespace Features.Customers.Scripts.NPCStates
{
  public abstract class NPCStateMachineState : BaseStateMachineState
  {
    private readonly NPCStateMachineObserver npc;
    private readonly SimpleAnimator animator;

    protected NPCStateMachineState(NPCStateMachineObserver npc, SimpleAnimator animator)
    {
      this.npc = npc;
      this.animator = animator;
    }

    public virtual void UpdateState(in float deltaTime)
    {
      
    }

    protected void SetBool(int hash, bool value) => 
      animator.SetBool(hash, value);
  }
}