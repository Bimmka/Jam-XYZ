﻿using Cinemachine;
using Features.Animation;
using Features.Player.Scripts.AreaChange;
using Features.Player.Scripts.Base;
using Features.Player.Scripts.Camera;
using Features.Player.Scripts.HeroMachine;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.Markers;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Rotate;
using Features.Player.Scripts.Steal;
using Features.Services.Coroutine;
using Features.Services.Input;
using Features.StaticData.Hero.AnimationTransitions;
using Features.StaticData.Hero.Camera;
using Features.StaticData.Hero.Move;
using Features.StaticData.Hero.NPCSearching;
using Features.StaticData.Hero.Rotate;
using Features.StaticData.Hero.Stealing;
using Features.StaticData.LevelArea;
using InputControl;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class HeroSceneBootstrapp : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private GameObject hero;
    [SerializeField] private Transform spawnHeroPoint;
    [SerializeField] private HeroMoveStaticData moveData;
    [SerializeField] private HeroRotateStaticData rotateData;
    [SerializeField] private HeroAnimationsTransitionStaticData transitionStaticData;
    [SerializeField] private HeroCameraStaticData cameraStaticData;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cameraLookAtPoint;
    [SerializeField] private LevelStaticData levelStaticData;
    [SerializeField] private HeroStealingStaticData stealingStaticData;
    [SerializeField] private HeroNPCSearchingStaticData searchingStaticData;
    
    public override void InstallBindings()
    {
      BindInput();
      BindHeroCamera();
      BindHeroAreaChange();
      CreateHero();  
    }
    
    private void BindInput() => 
      Container.Bind<IInputService>().To<InputService>().FromNew().AsSingle().WithArguments(new HeroControl());
    
    private void BindHeroCamera() => 
      Container.Bind<HeroCamera>().To<HeroCamera>().FromNew().AsSingle().WithArguments(cameraStaticData, virtualCamera, cameraLookAtPoint);

    private void BindHeroAreaChange()
    {
      Container.Bind<HeroAreaChangeObserver>().To<HeroAreaChangeObserver>()
        .AsSingle()
        .WithArguments(levelStaticData.StartArea);
    }

    private void CreateHero()
    {
      GameObject spawnedHero = Container.InstantiatePrefab(hero, spawnHeroPoint.position, Quaternion.Euler(0,0,0), null);

      HeroRotate rotate = HeroRotate(spawnedHero);
      HeroMove move = HeroMove(rotate, spawnedHero);
            
            
      ChangeableParametersAnimator animator = spawnedHero.GetComponentInChildren<ChangeableParametersAnimator>(true);
      animator.Initialize();

      spawnedHero.GetComponent<HeroAreaChangeObserver>().SetStartArea(levelStaticData.StartArea);

      HeroInteractionSearchMarker searchMarker = spawnedHero.GetComponentInChildren<HeroInteractionSearchMarker>();
      
      HeroNPCSearcher heroNPCSearcher = new HeroNPCSearcher(searchMarker.transform, searchingStaticData, this);
      heroNPCSearcher.StartSearch();
      HeroStealPreparing heroStealPreparing = new HeroStealPreparing(stealingStaticData);
      HeroStatesContainer container = new HeroStatesContainer(spawnedHero.GetComponent<HeroStateMachineObserver>(), 
        move, animator, transitionStaticData, heroNPCSearcher, heroStealPreparing);

      spawnedHero.GetComponentInChildren<HeroStealDisplayer>().Construct(heroStealPreparing, stealingStaticData.MaxPrepareCount);
      
      spawnedHero.GetComponent<Hero>().Construct(container);
    }

    private HeroRotate HeroRotate(GameObject spawnedHero) => 
      new HeroRotate(spawnedHero.transform, rotateData);

    private HeroMove HeroMove(HeroRotate rotate, GameObject spawnedHero) => 
      new HeroMove(spawnedHero.transform, moveData, rotate, spawnedHero.GetComponent<Rigidbody2D>());
  }
}