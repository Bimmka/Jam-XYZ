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
    public Transform LookAtTarget;
    public LevelAreaType Area;
  }
}