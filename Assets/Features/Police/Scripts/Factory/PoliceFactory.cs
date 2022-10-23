using Features.Police.Data;
using Features.Police.Scripts.Path;
using Features.Police.Scripts.PoliceStates;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.Police.Scripts.Factory
{
  public class PoliceFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly Transform spawnParent;

    public PoliceFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, Transform spawnParent)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.spawnParent = spawnParent;
    }

    public PoliceOfficer Spawn(PoliceLevelData policeLevelData)
    {
      PoliceSettings settings = staticDataService.ForPolice(policeLevelData.Type);
      PoliceOfficer officer = assetProvider.Instantiate(settings.Prefab, policeLevelData.StartPosition, Quaternion.identity, spawnParent);
      PolicePathObserver pathObserver = new PolicePathObserver(policeLevelData.PatrolPath, policeLevelData.SearchPath);
      officer.Construct(settings, pathObserver);

      return officer;
    }
  }
}