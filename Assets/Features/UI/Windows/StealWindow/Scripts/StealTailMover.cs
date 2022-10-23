using Features.Extensions;
using UniRx;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class StealTailMover
  {
    private readonly Vector3 leftPoint;
    private readonly Vector3 rightPoint;
    private readonly float height;
    private readonly Transform downAnchor;

    public readonly ReactiveCommand SwitchDirection = new ReactiveCommand();

    public StealTailMover(Vector3 leftPoint, Vector3 rightPoint, float height, Transform downAnchor)
    {
      this.leftPoint = leftPoint;
      this.rightPoint = rightPoint;
      this.height = height;
      this.downAnchor = downAnchor;
    }

    public void Move(in float currentTime)
    {
      downAnchor.localPosition = TrajectoryExtensions.Parabola(leftPoint, rightPoint, -height, currentTime);
      
      if (downAnchor.localPosition.x >= leftPoint.x)
        NotifyAboutWentLeft();
      else if (downAnchor.localPosition.x <= rightPoint.x)
        NotifyAboutWentRight();
    }

    private void NotifyAboutWentLeft() =>
      SwitchDirection.Execute();

    private void NotifyAboutWentRight() => 
      SwitchDirection.Execute();
  }
}