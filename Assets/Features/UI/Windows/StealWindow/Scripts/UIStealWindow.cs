using Features.Extensions;
using Features.UI.Custom.LineRenderer;
using Features.UI.Windows.StealWindow.StealFillPatterns;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class UIStealWindow : MonoBehaviour
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

    private StealTailMover stealTailMover;
    private StealWindowItemFiller itemFiller;
    
    private readonly RaycastHit2D[] hits = new RaycastHit2D[1];
    private readonly Vector2[] lineRendererPositions = new Vector2[2];

    private int currentDirection = 1;
    private float currentTime;

    [Inject]
    public void Construct(StealItemFactory stealItemFactory)
    {
      itemFiller = new StealWindowItemFiller(stealItemFactory, spawnPositions, settings.MinCoinsCount);
    }

    private void Awake()
    {
      stealTailMover = new StealTailMover(rightPoint.localPosition, leftPoint.localPosition, 
        (leftPoint.localPosition - minDownPoint.localPosition).y, downAnchor);
      
      stealTailMover.WentLeft += OnWentLeftPosition;
      stealTailMover.WentRight += OnWentRightPosition;
      
      SpawnObjects();
    }

    private void OnDestroy()
    {
      stealTailMover.WentLeft -= OnWentLeftPosition;
      stealTailMover.WentRight -= OnWentRightPosition;
    }

    public void SpawnObjects()
    {
      StealWindowFillPattern pattern = settings.RandomPattern();
      StealWindowFillCell[] cells = new StealWindowFillCell[pattern.Cells.Length];
      pattern.Cells.CopyTo(cells, 0);
      cells.Shuffle();
      
      itemFiller.SpawnObjects(cells, pattern.EntitiesInPattern);
    }

    private void Update()
    {
      currentTime += currentDirection * Time.deltaTime;

      stealTailMover.Move(currentTime/timeToMoveAllCircle);

      float angle = -Vector2.SignedAngle(downAnchor.position - tail.position, Vector2.down);
      tail.eulerAngles = Vector3.forward * angle;

      if (IsHitItem())
        DrawLineToItem(hits[0]);
      else
        DrawLineDownAnchor();
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

    private void OnWentLeftPosition() => 
      currentDirection = 1;

    private void OnWentRightPosition() => 
      currentDirection = -1;
    
  }
  
}
