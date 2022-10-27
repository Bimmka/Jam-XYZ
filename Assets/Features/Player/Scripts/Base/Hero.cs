using System;
using Features.Alarm;
using Features.Animation;
using Features.Player.Scripts.Gold;
using Features.Player.Scripts.HeroMachine;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.InputControl;
using Features.Player.Scripts.Markers;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Rotate;
using Features.Player.Scripts.Steal;
using Features.Services.Coroutine;
using Features.Services.UI.Windows;
using Features.StaticData.Hero.AnimationTransitions;
using Features.StaticData.Hero.Move;
using Features.StaticData.Hero.NPCSearching;
using Features.StaticData.Hero.Rotate;
using Features.StaticData.StealItems;
using UnityEngine;
using Zenject;

namespace Features.Player.Scripts.Base
{
  [RequireComponent(typeof(HeroInput))]
  [RequireComponent(typeof(HeroStateMachineObserver))]
  [RequireComponent(typeof(Rigidbody2D))]
  public class Hero : MonoBehaviour
  {
    [SerializeField] private HeroInput input;
    [SerializeField] private HeroStateMachineObserver stateMachine;
    [SerializeField] private HeroStealObserver stealObserver;
    [SerializeField] private HeroAnimationsTransitionStaticData animationsTransition;
    [SerializeField] private ChangeableParametersAnimator animator;
    [SerializeField] private HeroRotateStaticData rotateData;
    [SerializeField] private HeroMoveStaticData heroMoveData;
    [SerializeField] private HeroInteractionSearchMarker startSearchPoint;
    [SerializeField] private HeroNPCSearchingStaticData searchingData;
    [SerializeField] private StealItemCostStaticData costStaticData;
    [SerializeField] private Rigidbody2D body;

    private bool isLocked;

    [Inject]
    public void Construct(HeroStealPreparing stealPreparing, IWindowsService windowsService,
      ICoroutineRunner coroutineRunner,
      HeroGold heroGold, NPCAlarm alarm)
    {
      HeroRotate heroRotate = new HeroRotate(transform, rotateData);
      HeroMove move = new HeroMove(transform, heroMoveData, heroRotate, body);
      HeroNPCSearcher heroNpcSearcher = new HeroNPCSearcher(startSearchPoint, searchingData, coroutineRunner);

      HeroStatesContainer container = new HeroStatesContainer(stateMachine, move, animator, animationsTransition,
        heroNpcSearcher, stealPreparing, windowsService, heroGold, alarm, costStaticData);

      stateMachine.Construct(container);
      stateMachine.Subscribe();
      stateMachine.CreateStates();
      heroNpcSearcher.StartSearch();

      isLocked = true;
    }

    private void Start()
    {
      animator.Initialize();
      stateMachine.SetDefaultState();
    }

    private void OnDestroy()
    {
      stateMachine.Cleanup();
      stealObserver.Cleanup();
      input.Cleanup();
    }

    private void Update()
    {
      if (isLocked)
        return;
      
      input.ReadInput();
      stateMachine.UpdateState(input.Commands, input.CommandsCount, Time.deltaTime);
      input.ClearInput();
    }

    public void Enable()
    {
      isLocked = false;
    }
  }
}