using Features.LevelArea.Scripts;
using Features.StaticData.LevelArea;
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
    }
        
    private void BindLevelBoundsMarkerFactory() => 
      Container.Bind<LevelBoundsFactory>().To<LevelBoundsFactory>().FromNew().AsSingle().WithArguments(levelStaticData.AreaBoundMarkerPrefab);

    private void BindLevelBoundsObserver() => 
      Container.Bind<LevelBoundsObserver>().To<LevelBoundsObserver>().FromNew().AsSingle().WithArguments(levelStaticData.AreaBounds);
  }
}