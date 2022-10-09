using Features.Services.UI.Factory;
using Features.StaticData.Customers;
using Features.StaticData.Windows;
using Features.StealItems.Scripts;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
    NPCSettings ForNPC(NPCType type);
    StealItem StealItem();
    Sprite StealItemView(StealItemType type);
    MovingBlock MovingObject();
  }
}