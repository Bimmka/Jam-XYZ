using System;
using Features.Customers.Scripts.NPCStates;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.Customers.Scripts.Base
{
    [RequireComponent(typeof(NPCStateMachineObserver))]
    public class NPC : MonoBehaviour
    {
        [SerializeField] private NPCStateMachineObserver stateMachine;
        
        private LevelAreaType spawnDataArea;

        public event Action<NPC, LevelAreaType> Exited;
        public event Action<NPC, LevelAreaType> Robbed;

        public void Construct(NPCStatesContainer container, LevelAreaType spawnDataArea)
        {
            this.spawnDataArea = spawnDataArea;
            stateMachine.Construct(container);
            stateMachine.Subscribe();
            stateMachine.CreateStates();
            stateMachine.SetDefaultState();
        }
        
        private void Update() => 
            stateMachine.UpdateState(Time.deltaTime);

        public void NotifyAboutExit() => 
            Exited?.Invoke(this, spawnDataArea);

        public void NotifyAboutRobbed() => 
            Robbed?.Invoke(this, spawnDataArea);
    }
}
