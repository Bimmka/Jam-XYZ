using System;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.StealFillPatterns
{
  [Serializable]
  public struct StealWindowFillCell
  {
    public Vector2Int Position;
    public StealCellAggregateType AggregateType;
  }
}