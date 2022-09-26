using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.Customers.Scripts.NPCStates;
using Features.Customers.Scripts.Timing;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.StaticData.Customers;
using Features.StaticData.LevelArea;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Features.Customers.Scripts.Factory
{
  public class NPCFactory
  {
    private readonly IStaticDataService staticData;
    private readonly IAssetProvider assetProvider;

    [Inject]
    public NPCFactory(IStaticDataService staticData, IAssetProvider assetProvider)
    {
      this.staticData = staticData;
      this.assetProvider = assetProvider;
    }

    public NPC Spawn(NPCSpawnData spawnData)
    {
      NPCSettings settings = staticData.ForNPC(spawnData.Type);
      NPC npc = assetProvider.Instantiate(settings.View, spawnData.SpawnPosition);
      
      AIPath path = npc.GetComponent<AIPath>();
      path.maxSpeed = settings.Speed;

      NPCAlertness alertness = Alertness(settings);
      NPCAlertnessObserver alertnessObserver = npc.GetComponent<NPCAlertnessObserver>();
      alertnessObserver.Construct(alertness);
      
      npc.Construct(StatesContainer(npc, path, alertnessObserver), spawnData.Area, alertness);
      
      NPCExistTimeObserver timeObserver = new NPCExistTimeObserver(Random.Range(settings.ExistSecondsRange.x, settings.ExistSecondsRange.y));

      return npc;
    }

    private NPCAlertness Alertness(NPCSettings settings) => 
      new NPCAlertness(settings.AlertnessPerSecond, settings.RelaxationPerSecond, settings.MaxAlertness);

    private NPCStatesContainer StatesContainer(NPC npc, AIPath aiPath, NPCAlertnessObserver alertnessObserver)
    {
      NPCStateMachineObserver stateMachine = npc.GetComponent<NPCStateMachineObserver>();
      SimpleAnimator animator = npc.GetComponent<SimpleAnimator>();
     
      AIDestinationSetter destinationSetter = npc.GetComponent<AIDestinationSetter>();
      return new NPCStatesContainer(stateMachine, animator, aiPath, destinationSetter, alertnessObserver);
    }
  }
}