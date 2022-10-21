using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;

namespace Features.StaticData.StealItems
{
  [CreateAssetMenu(fileName = "StealItemCostStaticData", menuName = "StaticData/Steal Item/Create Steal Item Cost", order = 52)]
  public class StealItemCostStaticData : ScriptableObject
  {
    public StealItemCost[] Costs;

    public int Cost(StealItemType type)
    {
      for (int i = 0; i < Costs.Length; i++)
      {
        if (Costs[i].Type == type)
          return Costs[i].Cost;
      }

      return 0;
    }
  }
}