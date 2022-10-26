using System.Collections.Generic;
using System.Linq;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.LevelArea.Scripts.PointsOfInterest
{
  public class LevelPointsOfInterestObserver
  {
    private readonly Dictionary<LevelAreaType, Vector3[]> exits;
    
    public LevelPointsOfInterestObserver(LevelStaticData levelStaticData)
    {
      exits = levelStaticData.Exits.ToDictionary(x => x.Area, x => x.Positions);
    }

    public Vector3 NearestExit(LevelAreaType area, Vector3 startPosition)
    {
      Vector3[] exitsInArea = exits[area];

      if (exitsInArea.Length > 1)
        return NearestExit(exitsInArea, startPosition);
      else
        return exitsInArea[0];
    }

    private Vector3 NearestExit(Vector3[] exitsInArea, Vector3 startPosition)
    {
      Vector3 currentExitPosition = exitsInArea[0];
      float currentDistance = Vector3.Distance(startPosition, currentExitPosition);
      float calculatedDistance;
      for (int i = 1; i < exitsInArea.Length; i++)
      {
        calculatedDistance = Vector3.Distance(exitsInArea[i], startPosition);
        if (calculatedDistance < currentDistance)
        {
          currentDistance = calculatedDistance;
          currentExitPosition = exitsInArea[i];
        }
      }

      return currentExitPosition;
    }
  }
}