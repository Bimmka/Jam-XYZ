using System;
using Features.Extensions;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class StealTailMover
  {
    private readonly Vector3 leftPoint;
    private readonly Vector3 rightPoint;
    private readonly float height;
    private readonly Transform downAnchor;

    public event Action WentLeft;
    public event Action WentRight;

    public StealTailMover(Vector3 leftPoint, Vector3 rightPoint, float height, Transform downAnchor)
    {
      this.leftPoint = leftPoint;
      this.rightPoint = rightPoint;
      this.height = height;
      this.downAnchor = downAnchor;
    }

    public void Move(in float currentTime)
    {
      downAnchor.position = TrajectoryExtensions.Parabola(leftPoint, rightPoint, -height, currentTime);
      
      if (downAnchor.position.x >= leftPoint.x)
        NotifyAboutWentLeft();
      else if (downAnchor.position.x <= rightPoint.x)
        NotifyAboutWentRight();
    }

    private void NotifyAboutWentLeft() => 
      WentLeft?.Invoke();

    private void NotifyAboutWentRight() => 
      WentRight?.Invoke();
  }
}