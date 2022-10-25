using Features.LevelArea.Scripts.ChangingArea;
using Features.LevelArea.Scripts.PointsOfInterest;
using Features.StaticData.LevelArea;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class LevelSceneBootstrapp : MonoInstaller
  {
    [SerializeField] private Transform levelBoundsSpawnParent;
    [SerializeField] private LevelStaticData levelStaticData;
        
    public override void InstallBindings()
    {
      BindLevelBoundsMarkerFactory();
      BindLevelBoundsObserver();
      BindLevelPointsOfInterestObserver();
    }

    private void BindLevelBoundsMarkerFactory() => 
      Container
      .Bind<LevelBoundsFactory>()
      .To<LevelBoundsFactory>()
      .FromNew().AsSingle()
      .WithArguments(levelStaticData.AreaBoundMarkerPrefab, levelBoundsSpawnParent);

    private void BindLevelBoundsObserver() => 
      Container.Bind<LevelBoundsObserver>().To<LevelBoundsObserver>().FromNew().AsSingle().WithArguments(levelStaticData.AreaBounds);

    private void BindLevelPointsOfInterestObserver() =>
      Container
        .Bind<LevelPointsOfInterestObserver>()
        .To<LevelPointsOfInterestObserver>()
        .FromNew()
        .AsSingle()
        .WithArguments(levelStaticData);
  }
}