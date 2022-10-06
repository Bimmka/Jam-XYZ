using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Services.UI.Factory;
using Features.StaticData.Customers;
using Features.StaticData.StealItems;
using Features.StaticData.Windows;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    private Dictionary<NPCType, NPCSettings> npcs;
    private StealItemsStaticData itemsStaticData;
    
    
    public void Load()
    {
      windows = Resources
        .Load<WindowsStaticData>(GameConstants.WindowsDataPath)
        .InstantiateData
        .ToDictionary(x => x.ID, x => x);

      npcs = Resources
        .LoadAll<NPCSettings>(GameConstants.NPCDataPath)
        .ToDictionary(x => x.Type, x => x);

      itemsStaticData = Resources.Load<StealItemsStaticData>(GameConstants.StealItemStaticData);
      
      Resources.UnloadUnusedAssets();
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();

    public NPCSettings ForNPC(NPCType type) =>
      npcs.TryGetValue(type, out NPCSettings staticData)
        ? staticData 
        : null;

    public GameObject StealEntity(StealItemType type) => 
      itemsStaticData.StealEntities[type];

    public GameObject MovingObject() => 
      itemsStaticData.MovingEntity;
  }
}