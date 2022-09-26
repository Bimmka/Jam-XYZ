using System.Collections.Generic;
using System.Linq;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.LevelArea.Scripts.PointsOfInterest
{
  public class LevelPointsOfInterestObserver
  {
    private readonly Dictionary<LevelAreaType, Transform[]> exits;
    
    public LevelPointsOfInterestObserver(LevelStaticData levelStaticData)
    {
      exits = levelStaticData.Exits.ToDictionary(x => x.Area, x => x.Positions);
    }

    public Transform NearestExit(LevelAreaType area, Vector3 startPosition)
    {
      Transform[] exitsInArea = exits[area];

      if (exitsInArea.Length > 1)
        return NearestExit(exitsInArea, startPosition);
      else
        return exitsInArea[0];
    }

    private Transform NearestExit(Transform[] exitsInArea, Vector3 startPosition)
    {
      Transform currentExitPosition = exitsInArea[0];
      float currentDistance = Vector3.Distance(startPosition, currentExitPosition.position);
      float calculatedDistance;
      for (int i = 1; i < exitsInArea.Length; i++)
      {
        calculatedDistance = Vector3.Distance(exitsInArea[i].position, startPosition);
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