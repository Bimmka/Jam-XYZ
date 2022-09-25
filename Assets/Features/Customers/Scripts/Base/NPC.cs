using Features.Customers.Scripts.NPCStates;
using UnityEngine;

namespace Features.Customers.Scripts.Base
{
    [RequireComponent(typeof(NPCStateMachineObserver))]
    public class NPC : MonoBehaviour
    {
        [SerializeField] private NPCStateMachineObserver stateMachine;
        public void Construct(NPCStatesContainer container)
        {
            stateMachine.Construct(container);
            stateMachine.Subscribe();
            stateMachine.CreateStates();
            stateMachine.SetDefaultState();
        }
        
        private void Update() => 
            stateMachine.UpdateState(Time.deltaTime);
    }
}
