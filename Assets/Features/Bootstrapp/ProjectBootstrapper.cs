using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Scripts.LevelTimer;
using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.Coroutine;
using Features.Services.StaticData;
using Features.Services.UI.Factory.BaseUI;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base;
using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class ProjectBootstrapper : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain loadingCurtain;
    
    private Game game;
    public override void InstallBindings()
    {
      BindAssetProvider();
      BindStaticData();
      BindLoadingCurtain();
      BindCoroutineRunner();
      BindSceneLoader();
      BindWindowsService();
      BindUIFactory();
      BindStateMachine();
      BindStealItemFactory();
      BindLevelTimer();
    }

    private void BindAssetProvider() => 
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();

    private void BindStaticData()
    {
      StaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();

      Container.Bind<IStaticDataService>().To<StaticDataService>().FromInstance(staticDataService).AsSingle();
    }

    private void BindLoadingCurtain() => 
      Container.Bind<LoadingCurtain>().To<LoadingCurtain>().FromComponentInNewPrefab(loadingCurtain).AsSingle();

    private void BindCoroutineRunner() => 
      Container.Bind<ICoroutineRunner>().To<ICoroutineRunner>().FromInstance(this).AsSingle();
    
    private void BindSceneLoader() => 
      Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();

    private void BindWindowsService() => 
      Container.Bind<IWindowsService>().To<WindowsService>().FromNew().AsSingle();

    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();

    private void BindStateMachine()
    {
      Container.Bind<BootstrapState>().To<BootstrapState>().FromNew().AsSingle();
      Container.Bind<GameLoadState>().To<GameLoadState>().FromNew().AsSingle();
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();
      Container.Bind<Game>().To<Game>().FromNew().AsSingle();
    }
    
    private void BindStealItemFactory() => 
      Container.Bind<StealItemFactory>().ToSelf().FromNew().AsSingle();
    
    private void BindLevelTimer() => 
      Container.Bind<LevelTimerObserver>().ToSelf().FromNew().AsSingle();
  }
}