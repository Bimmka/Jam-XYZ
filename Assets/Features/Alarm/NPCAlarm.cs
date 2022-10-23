using System;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.Alarm
{
  public class NPCAlarm
  {
    public event Action<LevelAreaType, Vector3> Invoked;
    
    public NPCAlarm()
    {
      
    }

    public void InvokeAlarm(LevelAreaType area, Vector3 position) => 
      Invoked?.Invoke(area, position);
  }
}