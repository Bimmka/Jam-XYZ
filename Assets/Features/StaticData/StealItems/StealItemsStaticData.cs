using System.Collections.Generic;
using Features.UI.Windows.StealWindow.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.StealItems
{
  [CreateAssetMenu(fileName = "StealItemStaticData", menuName = "StaticData/Steal/Create Steal Items", order = 52)]
  public class StealItemsStaticData : SerializedScriptableObject
  {
    public Dictionary<StealItemType, GameObject> StealEntities;
    public GameObject MovingEntity;
  }
}