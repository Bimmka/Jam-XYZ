using Features.LevelArea.Scripts;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/Level/Create Level Data", order = 52)]
  public class LevelStaticData : ScriptableObject
  {
    public AreaBound[] AreaBounds;
    public ChangeLevelAreaMarker AreaBoundMarkerPrefab;

    public NPCSpawnData[] NPCs;
    public AreaExitPosition[] Exits;
  }
}