using Features.NPC.Scripts.NPCStates;
using UnityEngine;

namespace Features.NPC.Scripts.Base
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
