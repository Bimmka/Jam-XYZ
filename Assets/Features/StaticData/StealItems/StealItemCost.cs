using System;
using Features.UI.Windows.StealWindow.Scripts;

namespace Features.StaticData.StealItems
{
  [Serializable]
  public struct StealItemCost
  {
    public StealItemType Type;
    public int Cost;
  }
}