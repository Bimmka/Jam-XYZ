using Features.Level.Scripts.ChangingArea;
using Features.Level.Scripts.Flow;
using Features.Level.Scripts.Goal;
using Features.Level.Scripts.LevelTimer;
using Features.Level.Scripts.PointsOfInterest;
using Features.Services.UI.Factory.BaseUI;
using Features.StaticData.LevelArea;
using Features.UI.Windows.Base;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class LevelSceneBootstrapp : MonoInstaller
  {
    [SerializeField] private Transform levelBoundsSpawnParent;
    [SerializeField] private LevelStaticData levelStaticData;

    public override void Start()
    {
      base.Start();
      Container.Resolve<IUIFactory>();
      Container.Resolve<LevelFlowObserver>().StartLevel();
    }

    public override void InstallBindings()
    {
      BindLevelBoundsMarkerFactory();
      BindLevelBoundsObserver();
      BindLevelPointsOfInterestObserver();
      BindStealItemFactory();
      BindLevelTimer();
      BindUIFactory();
      BindLevelFlow();
      BindLevelGoal();
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
    
    private void BindStealItemFactory() => 
      Container.Bind<StealItemFactory>().ToSelf().FromNew().AsSingle();
    
    private void BindLevelTimer() => 
      Container.Bind<LevelTimerObserver>().ToSelf().FromNew().AsSingle();
    
    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();

    private void BindLevelFlow() => 
      Container.Bind<LevelFlowObserver>().ToSelf().FromNew().AsSingle().WithArguments(levelStaticData);

    private void BindLevelGoal() => 
      Container.Bind<LevelGoal>().ToSelf().FromNew().AsSingle().WithArguments(levelStaticData.GoalRanges);
  }
}