using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.StealFillPatterns
{
  [CreateAssetMenu(fileName = "StealWindowFillPattern", menuName = "StaticData/Steal/Create Filling Pattern", order = 52)]
  public class StealWindowFillPattern : ScriptableObject
  {
    [ValidateInput("Validate", "Has moving blocks and entity in the same row")]
    public StealWindowFillCell[] Cells;

    [ReadOnly, PropertySpace(SpaceAfter = 10)]
    public int EntitiesInPattern;

    [Button("Update Pattern")]
    private void UpdatePattern()
    {
      ValidatePattern();
    }

    private bool Validate(StealWindowFillCell[] cells)
    {
      int maxRow = cells.Max(x => x.Position.y);
      bool isHaveMovingObject = false;
      bool isHaveEntity = false;
      for (int i = 0; i < maxRow; i++)
      {
        IEnumerable<StealWindowFillCell> cellsInRow = cells.Where(x => x.Position.y == i);

        foreach (StealWindowFillCell cell in cellsInRow)
        {
          if (cell.AggregateType == StealCellAggregateType.Entity)
            isHaveEntity = true;

          if (isHaveMovingObject && cell.AggregateType == StealCellAggregateType.MovingBlock)
            return false;

          if (cell.AggregateType == StealCellAggregateType.MovingBlock)
            isHaveMovingObject = true;
        }

        if (isHaveEntity && isHaveMovingObject)
          return false;

        isHaveEntity = false;
        isHaveMovingObject = false;
      }

      return true;
    }

    private void CalculateEntitiesCount()
    {
      if (Cells == null)
        return;

      EntitiesInPattern = Cells.Count(x => x.AggregateType == StealCellAggregateType.Entity);
    }

    private void ValidatePattern()
    {
      CalculateEntitiesCount();
    }
  }
}