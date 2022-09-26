using Features.Animation;
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
      npc.Construct(StatesContainer(npc, path), spawnData.Area);
      
      NPCExistTimeObserver timeObserver = new NPCExistTimeObserver(Random.Range(settings.ExistSecondsRange.x, settings.ExistSecondsRange.y));

      return npc;
    }

    private NPCStatesContainer StatesContainer(NPC npc, AIPath aiPath)
    {
      NPCStateMachineObserver stateMachine = npc.GetComponent<NPCStateMachineObserver>();
      SimpleAnimator animator = npc.GetComponent<SimpleAnimator>();
     
      AIDestinationSetter destinationSetter = npc.GetComponent<AIDestinationSetter>();
      return new NPCStatesContainer(stateMachine, animator, aiPath, destinationSetter);
    }
  }
}