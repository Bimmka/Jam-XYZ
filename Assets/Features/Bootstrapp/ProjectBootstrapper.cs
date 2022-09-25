using Features.GameStates;
using Features.GameStates.States;
using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.Coroutine;
using Features.Services.StaticData;
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
      BindStateMachine();
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

    private void BindStateMachine()
    {
      Container.Bind<BootstrapState>().To<BootstrapState>().FromNew().AsSingle();
      Container.Bind<GameLoadState>().To<GameLoadState>().FromNew().AsSingle();
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();
      Container.Bind<Game>().To<Game>().FromNew().AsSingle();
    }
  }
}