using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Police.Data;
using Features.Services.UI.Factory;
using Features.StaticData.Customers;
using Features.StaticData.StealItems;
using Features.StaticData.Windows;
using Features.StealItems.Scripts;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    private Dictionary<NPCType, NPCSettings> npcs;
    private PolicesContainerSettings policesContainerSettings;
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

      policesContainerSettings = Resources.Load<PolicesContainerSettings>(GameConstants.PoliceStaticData);
      
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

    public StealItem StealItem() => 
      itemsStaticData.StealItem;

    public Sprite StealItemView(StealItemType type) => 
      itemsStaticData.StealItemsView[type];

    public MovingBlock MovingObject() => 
      itemsStaticData.MovingEntity;

    public PoliceSettings ForPolice(PoliceType policeType) => 
      policesContainerSettings.Settings[policeType];
  }
}