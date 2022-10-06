using Features.Services.UI.Factory;
using Features.StaticData.Customers;
using Features.StaticData.Windows;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
    NPCSettings ForNPC(NPCType type);
    GameObject StealEntity(StealItemType type);
    GameObject MovingObject();
  }
}