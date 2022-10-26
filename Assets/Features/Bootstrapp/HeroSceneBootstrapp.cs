using Cinemachine;
using Features.Player.Scripts.AreaChange;
using Features.Player.Scripts.Base;
using Features.Player.Scripts.Camera;
using Features.Player.Scripts.Gold;
using Features.Player.Scripts.HeroMachine;
using Features.Player.Scripts.Markers;
using Features.Player.Scripts.Steal;
using Features.Services.Coroutine;
using Features.Services.Input;
using Features.StaticData.Hero.Camera;
using Features.StaticData.Hero.NPCSearching;
using Features.StaticData.Hero.Stealing;
using Features.StaticData.LevelArea;
using Features.UI.Windows.GameMenu;
using InputControl;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class HeroSceneBootstrapp : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private GameObject hero;
    [SerializeField] private Transform spawnHeroPoint;
    [SerializeField] private HeroCameraStaticData cameraStaticData;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cameraLookAtPoint;
    [SerializeField] private LevelStaticData levelStaticData;
    [SerializeField] private HeroStealingStaticData stealingStaticData;
    [SerializeField] private HeroNPCSearchingStaticData searchingStaticData;
    [SerializeField] private UIHUD hud;
    
    public override void InstallBindings()
    {
      BindInput();
      BindHeroCamera();
      BindHeroAreaChange();
      BindHero();
      BindHeroGold();
      BindHeroNPCSearcher();
      BindHeroInteractionSearchMarker();
      BindHeroStateMachineObserver();
      BindHeroStealPreparing();
      BindHeroStealDisplayer();
    }

    private void BindInput() => 
      Container.Bind<IInputService>().To<InputService>().FromNew().AsSingle().WithArguments(new HeroControl());

    private void BindHeroGold() => 
      Container.Bind<HeroGold>().To<HeroGold>().FromNew().AsSingle();

    private void BindHeroCamera() => 
      Container.Bind<HeroCamera>().To<HeroCamera>().FromNew().AsSingle().WithArguments(cameraStaticData, virtualCamera, cameraLookAtPoint);

    private void BindHeroAreaChange() => 
      Container.Bind<HeroAreaChanger>().ToSelf().FromNew().AsSingle().WithArguments(levelStaticData.StartArea);

    private void BindHero() => 
      Container.Bind<Hero>().To<Hero>().FromComponentInNewPrefab(hero).AsSingle();

    private void BindHeroStateMachineObserver() => 
      Container.Bind<HeroStateMachineObserver>().To<HeroStateMachineObserver>().FromComponentInNewPrefab(hero).AsSingle();

    private void BindHeroNPCSearcher() => 
      Container.Bind<HeroNPCSearcher>().To<HeroNPCSearcher>().FromNew().AsSingle().WithArguments(searchingStaticData);

    private void BindHeroInteractionSearchMarker() =>
      Container.Bind<HeroInteractionSearchMarker>().To<HeroInteractionSearchMarker>().FromComponentInNewPrefab(hero)
        .AsSingle();

    private void BindHeroStealPreparing() => 
      Container.Bind<HeroStealPreparing>().To<HeroStealPreparing>().FromNew().AsSingle().WithArguments(stealingStaticData);
    
    private void BindHeroStealDisplayer() =>
      Container.Bind<HeroStealDisplayer>().To<HeroStealDisplayer>().FromComponentInNewPrefab(hero)
        .AsSingle().WithArguments(stealingStaticData.MaxPrepareCount);
    
    public override void Start()
    {
      base.Start();
      SpawnHero();
      SpawnHUD();
    }

    private void SpawnHero() => 
      Container.InstantiatePrefab(hero, spawnHeroPoint.position, Quaternion.Euler(0,0,0), null);

    private void SpawnHUD() => 
      Container.InstantiatePrefab(hud);
  }
}