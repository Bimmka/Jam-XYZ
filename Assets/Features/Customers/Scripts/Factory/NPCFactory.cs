using Features.Animation;
using Features.Customers.Scripts.Base;
using Features.Customers.Scripts.NPCStates;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.StaticData.Customers;
using Features.StaticData.LevelArea;
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
      
      npc.Construct(StatesContainer(npc));

      return npc;
    }

    private NPCStatesContainer StatesContainer(NPC npc)
    {
      NPCStateMachineObserver stateMachine = npc.GetComponent<NPCStateMachineObserver>();
      SimpleAnimator animator = npc.GetComponent<SimpleAnimator>();
      return new NPCStatesContainer(stateMachine, animator);
    }
  }
}