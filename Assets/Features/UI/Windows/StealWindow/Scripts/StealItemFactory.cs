using Features.Services.Assets;
using Features.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class StealItemFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;

    [Inject]
    public StealItemFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
    }
    
    public void SpawnEntity(StealItemType type, Transform parent)
    {
      assetProvider.Instantiate(staticDataService.StealEntity(type), parent);
    }

    public void SpawnMovingBlock(Transform parent)
    {
      assetProvider.Instantiate(staticDataService.MovingObject(), parent);
    }
  }
}