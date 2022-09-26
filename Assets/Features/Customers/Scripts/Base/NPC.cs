using System;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.NPCStates;
using Features.Customers.Scripts.Timing;
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

        public void Construct(NPCStatesContainer container, LevelAreaType spawnDataArea, NPCAlertness alertness,
            NPCExistTimeObserver existTimeObserver)
        {
            this.spawnDataArea = spawnDataArea;
            stateMachine.Leaved += NotifyAboutExit;
            stateMachine.Robbed += NotifyAboutRobbed;
            stateMachine.Construct(container, alertness, existTimeObserver);
            stateMachine.Subscribe();
            stateMachine.CreateStates();
            stateMachine.SetDefaultState();
        }

        private void OnDestroy()
        {
            stateMachine.Leaved -= NotifyAboutExit;
            stateMachine.Robbed -= NotifyAboutRobbed;
            stateMachine.Cleanup();
        }

        private void Update() => 
            stateMachine.UpdateState(Time.deltaTime);

        public void NotifyAboutExit() => 
            Exited?.Invoke(this, spawnDataArea);

        public void NotifyAboutRobbed() => 
            Robbed?.Invoke(this, spawnDataArea);
    }
}
