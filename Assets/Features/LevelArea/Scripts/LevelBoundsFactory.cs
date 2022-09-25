using Features.Services.Assets;
using Features.StaticData.LevelArea;
using UnityEngine;
using Zenject;

namespace Features.LevelArea.Scripts
{
  public class LevelBoundsFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly ChangeLevelAreaMarker prefab;

    [Inject]
    public LevelBoundsFactory(IAssetProvider assetProvider, ChangeLevelAreaMarker prefab)
    {
      this.assetProvider = assetProvider;
      this.prefab = prefab;
    }

    public ChangeLevelAreaMarker Marker(Transform parent, LevelAreaType areaType)
    {
      ChangeLevelAreaMarker marker = assetProvider.Instantiate(prefab, parent);
      marker.Initialize(areaType);
      return marker;
    }
  }
}