using Features.Services.Assets;
using Features.Services.StaticData;
using Features.StealItems.Scripts;
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
    
    public StealItem SpawnEntity(StealItemType type, Transform parent)
    {
      StealItem item = assetProvider.Instantiate(staticDataService.StealItem(), parent);
      item.Initialize(type, staticDataService.StealItemView(type));
      return item;
    }

    public void SpawnMovingBlock(Transform parent, Transform leftPoint, Transform rightPoint)
    {
      MovingBlock movingBlock = assetProvider.Instantiate(staticDataService.MovingObject(), parent);
      movingBlock.Initialize(leftPoint, rightPoint);
      movingBlock.StartMove();
    }
  }
}