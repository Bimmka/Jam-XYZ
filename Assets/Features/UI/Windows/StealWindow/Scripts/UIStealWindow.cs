using System;
using System.Collections;
using Features.Extensions;
using Features.StealItems.Scripts;
using Features.UI.Custom.LineRenderer;
using Features.UI.Windows.Base;
using Features.UI.Windows.StealWindow.StealFillPatterns;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Windows.StealWindow.Scripts
{
  [RequireComponent(typeof(TimerDisplayer))]
  public class UIStealWindow : BaseWindow
  {
    [SerializeField] private TimerDisplayer timerDisplayer;
    [SerializeField] private StealWindowCell[] spawnPositions;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform minDownPoint;
    [SerializeField] private Transform downAnchor;
    [SerializeField] private Transform tail;
    [SerializeField] private float timeToMoveAllCircle = 2f;
    [SerializeField] private CustomUILineRenderer lineRenderer;
    [SerializeField] private StealWindowSettings settings;

    private readonly RaycastHit2D[] hits = new RaycastHit2D[1];

    private readonly CompositeDisposable disposable = new CompositeDisposable();
    
    private StealTailMover stealTailMover;
    private StealWindowItemFiller itemFiller;
    private TailLineDrawer lineDrawer;

    private Timer timer;

    private int currentDirection = 1;
    private float currentTime;

    private Coroutine moveTailCoroutine;

    private event Action<StealItemType> onHitGold;
    private event Action onHitRing;
    private event Action onMissed;
    private event Action onTimeOut;
    
    [Inject]
    public void Construct(StealItemFactory stealItemFactory)
    {
      itemFiller = new StealWindowItemFiller(stealItemFactory, spawnPositions, settings.MinCoinsCount);
      lineDrawer = new TailLineDrawer(tail, downAnchor, lineRenderer);
    }

    public void Initialize(float maxTime, Action<StealItemType> onHitGold, Action onHitRing, Action onMissed, Action onTimeOut)
    {
      timer = new Timer();
      timer.Start(maxTime);
      timer.TimeOut.Subscribe(onNext => OnTimeOut()).AddTo(disposable);
      timerDisplayer.Construct(timer);
      this.onHitGold = onHitGold;
      this.onHitRing = onHitRing;
      this.onMissed = onMissed;
      this.onTimeOut = onTimeOut;
    }

    public override void Open()
    {
      SpawnObjects();
      base.Open();
      moveTailCoroutine = StartCoroutine(MoveTail());
    }

    protected override void Initialize()
    {
      base.Initialize();
      stealTailMover = new StealTailMover(rightPoint.localPosition, leftPoint.localPosition, 
        (leftPoint.localPosition - minDownPoint.localPosition).y, downAnchor);
      
      stealTailMover.SwitchDirection.Subscribe(onNext => SwitchDirection()) .AddTo(disposable);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      disposable.Clear();
      timerDisplayer.Cleanup();
    }

    public void Catch()
    {
      StopCoroutine(moveTailCoroutine);
      if (IsHitItem())
      {
        if (IsHitGold())
          NotifyAboutHitGold();
        else
          NotifyAboutHitRing();
      }
      else
        NotifyAboutMiss();
      
      Destroy();
    }

    private bool IsHitGold()
    {
      StealItem coin = hits[0].collider.GetComponent<StealItem>();
      return coin.Type != StealItemType.Ring;
    }

    private void OnTimeOut()
    {
      StopCoroutine(moveTailCoroutine);
      NotifyAboutTimeOut();
      Destroy();
    }

    private void SpawnObjects()
    {
      StealWindowFillPattern pattern = settings.RandomPattern();
      StealWindowFillCell[] cells = new StealWindowFillCell[pattern.Cells.Length];
      pattern.Cells.CopyTo(cells, 0);
      cells.Shuffle();
      
      itemFiller.SpawnObjects(cells, pattern.EntitiesInPattern);
    }

    private IEnumerator MoveTail()
    {
      while (true)
      {
        currentTime += currentDirection * Time.deltaTime;

        stealTailMover.Move(currentTime/timeToMoveAllCircle);

        float angle = -Vector2.SignedAngle(downAnchor.position - tail.position, Vector2.down);
        tail.eulerAngles = Vector3.forward * angle;

        if (IsHitItem())
          lineDrawer.DrawLineToItem(transform.InverseTransformPoint(hits[0].point));
        else
          lineDrawer.DrawLineDownAnchor();

        yield return null;
      }
    }

    private bool IsHitItem()
    {
      Vector3 positionsDifference = downAnchor.position - tail.position;
      Vector3 direction = positionsDifference.normalized;
      float distance = positionsDifference.magnitude;
      return Physics2D.RaycastNonAlloc(tail.position, direction, hits, distance, settings.ItemsLayer) > 0;
    }

    private void SwitchDirection() => 
      currentDirection *= -1;

    private void NotifyAboutHitGold()
    {
      StealItem coin = hits[0].collider.GetComponent<StealItem>();
      onHitGold?.Invoke(coin.Type);
    }

    private void NotifyAboutHitRing() => 
      onHitRing?.Invoke();

    private void NotifyAboutMiss() => 
      onMissed?.Invoke();

    private void NotifyAboutTimeOut() => 
      onTimeOut?.Invoke();
  }
}
