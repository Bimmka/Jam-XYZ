using System.Collections.Generic;
using Features.Customers.Scripts.Base;
using Features.StaticData.LevelArea;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Customers.Scripts.Factory
{
  public class NPCObserver
  {
    private readonly NPCFactory factory;
    private readonly Dictionary<LevelAreaType, List<NPC>> npcsByArea;

    public readonly BoolReactiveProperty IsEnded = new BoolReactiveProperty(false);

    [Inject]
    public NPCObserver(NPCFactory factory)
    {
      this.factory = factory;
      
      npcsByArea = new Dictionary<LevelAreaType, List<NPC>>(5);
    }

    public void SpawnNPCs(NPCSpawnData[] spawnDatas)
    {
      NPC npc;
      for (int i = 0; i < spawnDatas.Length; i++)
      {
        npc = factory.Spawn(spawnDatas[i]);
        SaveNPC(npc, spawnDatas[i].Area);
        MoveToStartPosition(npc, spawnDatas[i].LookAtTarget);
      }
    }

    private void SaveNPC(NPC npc, LevelAreaType area)
    {
      if (npcsByArea.ContainsKey(area) == false)
        npcsByArea.Add(area, new List<NPC>(5));

      npcsByArea[area].Add(npc);

      npc.Exited += OnExited;
      npc.Robbed += OnRobbed;
    }

    private void MoveToStartPosition(NPC npc, Transform target)
    {
      npc.GetComponent<NPCStateMachineObserver>().GoToStartPoint(target);  
    }

    private void OnExited(NPC npc, LevelAreaType area)
    {
      RemoveNPC(npc, area);

      if (IsEnd())
        NotifyAboutEnd();
    }

    private void OnRobbed(NPC npc, LevelAreaType area)
    {
      RemoveNPC(npc, area);

      if (IsEnd())
        NotifyAboutEnd();
    }

    private void RemoveNPC(NPC npc, LevelAreaType area)
    {
      npcsByArea[area].Remove(npc);

      if (npcsByArea[area].Count == 0)
        npcsByArea.Remove(area);

      npc.Exited -= OnExited;
      npc.Robbed -= OnRobbed;
    }

    private bool IsEnd() => 
      npcsByArea.Count == 0;

    private void NotifyAboutEnd() => 
      IsEnded.Value = true;
  }
}