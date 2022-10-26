using System.Collections.Generic;
using Features.StaticData.LevelArea;
using Zenject;

namespace Features.LevelArea.Scripts.ChangingArea
{
  public class LevelBoundsObserver
  {
    private readonly LevelBoundsFactory factory;
    private readonly Dictionary<LevelAreaType, List<ChangeLevelAreaMarker>> markersByArea;
    
    [Inject]
    public LevelBoundsObserver(LevelBoundsFactory factory, AreaBound[] areaBounds)
    {
      this.factory = factory;
      markersByArea = new Dictionary<LevelAreaType, List<ChangeLevelAreaMarker>>(4);
      CreateAreaBounds(areaBounds);
    }

    public void DisableMarkersIn(LevelAreaType area)
    {
      for (int i = 0; i < markersByArea[area].Count; i++)
      {
        markersByArea[area][i].Disable();
      }
    }

    public void EnableMarkersIn(LevelAreaType area)
    {
      for (int i = 0; i < markersByArea[area].Count; i++)
      {
        markersByArea[area][i].Enable();
      }
    }

    private void CreateAreaBounds(AreaBound[] markers)
    {
      ChangeLevelAreaMarker marker;
      for (int i = 0; i < markers.Length; i++)
      {
        marker = NewMarker(markers[i]);
        if (markersByArea.ContainsKey(marker.Area) == false)
          markersByArea.Add(marker.Area, new List<ChangeLevelAreaMarker>(4));
        
        markersByArea[markers[i].Area].Add(marker);
      }
    }

    private ChangeLevelAreaMarker NewMarker(AreaBound marker) => 
      factory.Marker(marker.SpawnPosition, marker.Area);
  }
}