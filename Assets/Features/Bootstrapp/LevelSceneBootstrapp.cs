using Features.LevelArea.Scripts.ChangingArea;
using Features.LevelArea.Scripts.PointsOfInterest;
using Features.StaticData.LevelArea;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class LevelSceneBootstrapp : MonoInstaller
  {
    [SerializeField] private LevelStaticData levelStaticData;
        
    public override void InstallBindings()
    {
      BindLevelBoundsMarkerFactory();
      BindLevelBoundsObserver();
      BindLevelPointsOfInterestObserver();
      BindStealFactory();
    }

    private void BindLevelBoundsMarkerFactory() => 
      Container.Bind<LevelBoundsFactory>().To<LevelBoundsFactory>().FromNew().AsSingle().WithArguments(levelStaticData.AreaBoundMarkerPrefab);

    private void BindLevelBoundsObserver() => 
      Container.Bind<LevelBoundsObserver>().To<LevelBoundsObserver>().FromNew().AsSingle().WithArguments(levelStaticData.AreaBounds);
    
    private void BindStealFactory() => 
      Container.Bind<StealItemFactory>().To<StealItemFactory>().FromNew().AsSingle();

    private void BindLevelPointsOfInterestObserver() =>
      Container
        .Bind<LevelPointsOfInterestObserver>()
        .To<LevelPointsOfInterestObserver>()
        .FromNew()
        .AsSingle()
        .WithArguments(levelStaticData);
  }
}