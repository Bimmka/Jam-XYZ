using System;
using Features.StaticData.Customers;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [Serializable]
  public struct NPCSpawnData
  {
    public NPCType Type;
    public Vector3 SpawnPosition;
    public Vector3 LookAtTarget;
    public LevelAreaType Area;

    public NPCSpawnData(NPCType type, Vector3 spawnPosition, Vector3 lookAtTarget, LevelAreaType area)
    {
      Type = type;
      SpawnPosition = spawnPosition;
      LookAtTarget = lookAtTarget;
      Area = area;
    }
  }
}