using System.Collections;
using Features.Extensions;
using Features.Services.Input;
using Features.UI.Custom.LineRenderer;
using Features.UI.Windows.Base;
using Features.UI.Windows.StealWindow.StealFillPatterns;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class UIStealWindow : BaseWindow
  {
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
    private readonly Vector2[] lineRendererPositions = new Vector2[2];
    private readonly CompositeDisposable disposable = new CompositeDisposable();
    
    private StealTailMover stealTailMover;
    private StealWindowItemFiller itemFiller;

    private Timer timer;

    private int currentDirection = 1;
    private float currentTime;

    [Inject]
    public void Construct(StealItemFactory stealItemFactory)
    {
      itemFiller = new StealWindowItemFiller(stealItemFactory, spawnPositions, settings.MinCoinsCount);
    }

    public void Initialize(float maxTime)
    {
      timer = new Timer(maxTime);
      timer.TimeOut.Subscribe(onNext => OnTimeOut()).AddTo(disposable);
    }

    public override void Open()
    {
      base.Open();
      StartCoroutine(MoveTail());
    }

    protected override void Initialize()
    {
      base.Initialize();
      stealTailMover = new StealTailMover(rightPoint.localPosition, leftPoint.localPosition, 
        (leftPoint.localPosition - minDownPoint.localPosition).y, downAnchor);
      
      stealTailMover.SwitchDirection.Subscribe(onNext => SwitchDirection()) .AddTo(disposable);

      SpawnObjects();
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      disposable.Clear();
    }

    public void Catch()
    {
      
    }

    private void OnTimeOut()
    {
      
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
          DrawLineToItem(hits[0]);
        else
          DrawLineDownAnchor();

        yield return null;
      }
    }

    private void DrawLineToItem(RaycastHit2D hit)
    {
      SetLineRendererPositions(tail.localPosition, transform.InverseTransformPoint(hit.point));
      lineRenderer.SetPoints(lineRendererPositions);
    }

    private void DrawLineDownAnchor()
    {
      SetLineRendererPositions(tail.localPosition, downAnchor.localPosition);
      lineRenderer.SetPoints(lineRendererPositions);
    }

    private bool IsHitItem()
    {
      Vector3 positionsDifference = downAnchor.position - tail.position;
      Vector3 direction = positionsDifference.normalized;
      float distance = positionsDifference.magnitude;
      return Physics2D.RaycastNonAlloc(tail.position, direction, hits, distance, settings.ItemsLayer) > 0;
    }

    private void SetLineRendererPositions(Vector2 at, Vector2 to)
    {
      lineRendererPositions[0] = at;
      lineRendererPositions[1] = to;
    }

    private void SwitchDirection() => 
      currentDirection *= -1;
  }
}
