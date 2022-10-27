using Features.Services.Assets;
using Features.StaticData.LevelArea;
using UnityEngine;
using Zenject;

namespace Features.Level.Scripts.ChangingArea
{
  public class LevelBoundsFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly ChangeLevelAreaMarker prefab;
    private readonly Transform spawnParent;

    [Inject]
    public LevelBoundsFactory(IAssetProvider assetProvider, ChangeLevelAreaMarker prefab, Transform spawnParent)
    {
      this.assetProvider = assetProvider;
      this.prefab = prefab;
      this.spawnParent = spawnParent;
    }

    public ChangeLevelAreaMarker Marker(Vector3 spawnPosition, LevelAreaType areaType)
    {
      ChangeLevelAreaMarker marker = assetProvider.Instantiate(prefab, spawnPosition, Quaternion.identity, spawnParent);
      marker.Initialize(areaType);
      return marker;
    }
  }
}