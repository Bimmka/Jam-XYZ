using System.Collections.Generic;
using Features.Alarm;
using Features.Police.Scripts.Factory;
using Features.Police.Scripts.PoliceStates;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.Police.Scripts.Observer
{
  public class PoliceObserver
  {
    private readonly PoliceFactory factory;
    private readonly NPCAlarm npcAlarm;

    private readonly Dictionary<LevelAreaType, List<PoliceStateMachineObserver>> officersByArea;

    public PoliceObserver(PoliceFactory factory, NPCAlarm npcAlarm)
    {
      this.factory = factory;
      this.npcAlarm = npcAlarm;
      this.npcAlarm.Invoked += OnAlarm;
      officersByArea = new Dictionary<LevelAreaType, List<PoliceStateMachineObserver>>(3);
    }

    public void Cleanup() => 
      npcAlarm.Invoked -= OnAlarm;

    public void Spawn(PoliceLevelData[] policeLevelDatas)
    {
      for (int i = 0; i < policeLevelDatas.Length; i++)
      {
        SavePolice(policeLevelDatas[i].Area, factory.Spawn(policeLevelDatas[i]));
      }
    }

    private void SavePolice(LevelAreaType areaType, PoliceStateMachineObserver officer)
    {
      if (officersByArea.ContainsKey(areaType) == false)
        officersByArea.Add(areaType, new List<PoliceStateMachineObserver>(4));
      
      officersByArea[areaType].Add(officer);
    }

    private void OnAlarm(LevelAreaType areaType, Vector3 position)
    {
      if (officersByArea.ContainsKey(areaType) == false)
        return;

      for (int i = 0; i < officersByArea[areaType].Count; i++)
      {
        officersByArea[areaType][i].GoToRobbedGuest(position);
      }
    }
  }
}