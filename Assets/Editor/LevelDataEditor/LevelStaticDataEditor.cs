using System.Collections.Generic;
using System.Linq;
using Features.Level.Scripts.Markers;
using Features.StaticData.LevelArea;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor.LevelDataEditor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private LevelStaticData data;
    
    public override void OnInspectorGUI()
    {
      data = (LevelStaticData) target;
      base.OnInspectorGUI();
      if (GUILayout.Button("Collect Markers"))
      {
        CollectMarkers();
        UpdateGameSceneName();
      }
    }

    private void CollectMarkers()
    {
      CollectAreaBounds();
      CollectNPCMarkers();
      CollectExits();
      CollectPoliceMarkers();
      
      EditorUtility.SetDirty(data);
    }

    private void CollectAreaBounds()
    {
      LevelChangeAreaMarker[] markers = FindObjectsOfType<LevelChangeAreaMarker>();

      data.AreaBounds = new AreaBound[markers.Length];

      for (int i = 0; i < markers.Length; i++)
      {
        data.AreaBounds[i] = new AreaBound(markers[i].transform.position, markers[i].AreaType);
      }
    }

    private void CollectNPCMarkers()
    {
      LevelNPCMarker[] markers = FindObjectsOfType<LevelNPCMarker>();
      
      data.NPCs = new NPCSpawnData[markers.Length];

      for (int i = 0; i < markers.Length; i++)
      {
        data.NPCs[i] = new NPCSpawnData(markers[i].NPCType, markers[i].SpawnPosition.position, 
          markers[i].PointOfInterestPosition.position, markers[i].AreaType);
      }
    }

    private void CollectExits()
    {
      LevelExitMarker[] markers = FindObjectsOfType<LevelExitMarker>();
      
      Dictionary<LevelAreaType, List<Vector3>> exitsByArea = new Dictionary<LevelAreaType, List<Vector3>>(4);

      for (int i = 0; i < markers.Length; i++)
      {
        if (exitsByArea.ContainsKey(markers[i].AreaType) == false)
          exitsByArea.Add(markers[i].AreaType, new List<Vector3>(10));
        
        exitsByArea[markers[i].AreaType].Add(markers[i].transform.position);
      }

      data.Exits = new AreaExitPosition[exitsByArea.Count];
      int index = 0;

      foreach (KeyValuePair<LevelAreaType,List<Vector3>> exit in exitsByArea)
      {
        data.Exits[index] = new AreaExitPosition(exit.Key, exit.Value.ToArray());
        index++;
      }
    }

    private void CollectPoliceMarkers()
    {
      LevelPoliceMarker[] markers = FindObjectsOfType<LevelPoliceMarker>();
      
      data.Polices = new PoliceLevelData[markers.Length];

      for (int i = 0; i < markers.Length; i++)
      {
        data.Polices[i] = new PoliceLevelData(markers[i].PoliceType, markers[i].AreaType, markers[i].SpawnPosition.position, 
          ToPositions(markers[i].PatrolPath), ToPositions(markers[i].SearchPath));
      }
    }

    private Vector3[] ToPositions(Transform[] transforms)
    {
      Vector3[] positions = new Vector3[transforms.Length];

      for (int i = 0; i < transforms.Length; i++)
      {
        positions[i] = transforms[i].position;
      }

      return positions;
    }

    private void UpdateGameSceneName() => 
      data.SceneName = SceneManager.GetActiveScene().name;
  }
}