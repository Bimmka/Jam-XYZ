using System;
using Features.UI.Custom.LineRenderer;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class UIStealWindow : MonoBehaviour
  {
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform downAnchor;
    [SerializeField] private Transform tail;
    [SerializeField] private float timeToMoveAllCircle = 2f;
    [SerializeField] private float height;
    [SerializeField] private CustomUILineRenderer lineRenderer;
    
    private StealTailMover stealTailMover;
    
    private int currentDirection = 1;
    private float currentTime;

    private void Awake()
    {
      stealTailMover = new StealTailMover(rightPoint.position, leftPoint.position, height, downAnchor);
      stealTailMover.WentLeft += OnWentLeftPosition;
      stealTailMover.WentRight += OnWentRightPosition;
    }

    private void OnDestroy()
    {
      stealTailMover.WentLeft -= OnWentLeftPosition;
      stealTailMover.WentRight -= OnWentRightPosition;
    }

    private void Update()
    {
      currentTime += currentDirection * Time.deltaTime;

      stealTailMover.Move(currentTime/timeToMoveAllCircle);

      float angle = -Vector2.SignedAngle(downAnchor.position - tail.position, Vector2.down);
      tail.eulerAngles = Vector3.forward * angle;
      
      lineRenderer.SetPoints(new Vector2[]{tail.localPosition, downAnchor.localPosition});
      
    }

    private void OnWentLeftPosition() => 
      currentDirection = 1;

    private void OnWentRightPosition() => 
      currentDirection = -1;
  }
  
  public class StealWindowFiller
  {
  
  }
}
