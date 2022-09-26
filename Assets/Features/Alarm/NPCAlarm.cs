using System;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.Alarm
{
  public class NPCAlarm
  {
    public event Action Invoked;
    
    public NPCAlarm()
    {
      
    }

    public void InvokeAlarm(LevelAreaType area, Vector3 position)
    {
      Invoked?.Invoke();
    }
  }
}