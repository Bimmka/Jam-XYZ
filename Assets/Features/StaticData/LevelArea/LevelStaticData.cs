using Features.Level.Scripts.ChangingArea;
using UnityEngine;

namespace Features.StaticData.LevelArea
{
  [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/Level/Create Level Data", order = 52)]
  public class LevelStaticData : ScriptableObject
  {
    public string SceneName;
    public AreaBound[] AreaBounds;
    public LevelAreaType StartArea;
    public ChangeLevelAreaMarker AreaBoundMarkerPrefab;

    public NPCSpawnData[] NPCs;
    public AreaExitPosition[] Exits;

    public PoliceLevelData[] Polices;

    public int SecondsForGame = 120;
    public GameGoalRanges GoalRanges;
  }
}