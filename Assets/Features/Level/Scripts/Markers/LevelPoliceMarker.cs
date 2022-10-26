using Features.Police.Data;
using UnityEngine;

namespace Features.LevelArea.Scripts.Markers
{
  public class LevelPoliceMarker : LevelMarker
  {
    public Transform SpawnPosition;
    public Transform[] PatrolPath;
    public Transform[] SearchPath;
    public PoliceType PoliceType;
  }
}