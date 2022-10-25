using Features.StaticData.Customers;
using UnityEngine;

namespace Features.LevelArea.Scripts.Markers
{
  public class LevelNPCMarker : LevelMarker
  {
    public Transform SpawnPosition;
    public Transform PointOfInterestPosition;
    public NPCType NPCType;
  }
}