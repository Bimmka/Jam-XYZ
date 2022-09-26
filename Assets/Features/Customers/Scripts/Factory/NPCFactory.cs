using Features.Animation;
using Features.Customers.Scripts.Alertness;
using Features.Customers.Scripts.Base;
using Features.Customers.Scripts.NPCStates;
using Features.Customers.Scripts.Timing;
using Features.LevelArea.Scripts.PointsOfInterest;
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
    private readonly LevelPointsOfInterestObserver pointsOfInterestObserver;

    [Inject]
    public NPCFactory(IStaticDataService staticData, IAssetProvider assetProvider, LevelPointsOfInterestObserver pointsOfInterestObserver)
    {
      this.staticData = staticData;
      this.assetProvider = assetProvider;
      this.pointsOfInterestObserver = pointsOfInterestObserver;
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
      
      NPCExistTimeObserver timeObserver = new NPCExistTimeObserver(Random.Range(settings.ExistSecondsRange.x, settings.ExistSecondsRange.y));

      npc.Construct(StatesContainer(npc, path, alertnessObserver, spawnData.Area), spawnData.Area, alertness, timeObserver);


      return npc;
    }

    private NPCAlertness Alertness(NPCSettings settings) => 
      new NPCAlertness(settings.AlertnessPerSecond, settings.RelaxationPerSecond, settings.MaxAlertness);

    private NPCStatesContainer StatesContainer(NPC npc, AIPath aiPath, NPCAlertnessObserver alertnessObserver, LevelAreaType area)
    {
      NPCStateMachineObserver stateMachine = npc.GetComponent<NPCStateMachineObserver>();
      SimpleAnimator animator = npc.GetComponentInChildren<SimpleAnimator>();
     
      AIDestinationSetter destinationSetter = npc.GetComponent<AIDestinationSetter>();
      return new NPCStatesContainer(stateMachine, animator, aiPath, destinationSetter, alertnessObserver, pointsOfInterestObserver, area);
    }
  }
}