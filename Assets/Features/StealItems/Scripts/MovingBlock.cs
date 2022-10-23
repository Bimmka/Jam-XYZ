using System;
using DG.Tweening;
using UnityEngine;

namespace Features.StealItems.Scripts
{
  public class MovingBlock : MonoBehaviour
  {
    [SerializeField] private float moveDuration;
    
    private Transform leftPoint;
    private Transform rightPoint;

    private Tweener moveTweener;
    
    public void Initialize(Transform leftPoint, Transform rightPoint)
    {
      this.rightPoint = rightPoint;
      this.leftPoint = leftPoint;
    }

    private void OnDestroy() => 
      moveTweener.Kill();

    public void StartMove()
    {
      if (IsLeftPointNear())
        MoveToRightPoint();
      else
        MoveToLeftPoint();
    }

    private void MoveToRightPoint() => 
      moveTweener = transform.DOMove(rightPoint.position, moveDuration).SetEase(Ease.Linear).OnComplete(MoveToLeftPoint);

    private void MoveToLeftPoint() => 
      moveTweener = transform.DOMove(leftPoint.position, moveDuration).SetEase(Ease.Linear).OnComplete(MoveToRightPoint);

    private bool IsLeftPointNear()
    {
      float leftXDifference = Mathf.Abs(transform.position.x - leftPoint.position.x);
      float rightXDifference = Mathf.Abs(transform.position.x - rightPoint.position.x);
      return leftXDifference < rightXDifference;
    }
  }
}