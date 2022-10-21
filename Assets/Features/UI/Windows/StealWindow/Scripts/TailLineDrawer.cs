using Features.UI.Custom.LineRenderer;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class TailLineDrawer
  {
    private readonly Transform tail;
    private readonly Transform downAnchor;
    private readonly CustomUILineRenderer lineRenderer;
    private readonly Vector2[] lineRendererPositions = new Vector2[2];

    public TailLineDrawer(Transform tail, Transform downAnchor, CustomUILineRenderer lineRenderer)
    {
      this.tail = tail;
      this.downAnchor = downAnchor;
      this.lineRenderer = lineRenderer;
    }
    
    public void DrawLineToItem(Vector3 endPosition)
    {
      SetLineRendererPositions(tail.localPosition, endPosition);
      lineRenderer.SetPoints(lineRendererPositions);
    }

    public void DrawLineDownAnchor()
    {
      SetLineRendererPositions(tail.localPosition, downAnchor.localPosition);
      lineRenderer.SetPoints(lineRendererPositions);
    }
    
    private void SetLineRendererPositions(Vector2 at, Vector2 to)
    {
      lineRendererPositions[0] = at;
      lineRendererPositions[1] = to;
    }

  }
}