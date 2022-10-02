using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Custom.LineRenderer
{
  public class CustomUILineRenderer  : Graphic
  {
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2[] points;
    [SerializeField] private float thickness = 10;

    private float width;
    private float height;
    private float unitWidth;
    private float unitHeight;

    public void SetPoints(Vector2[] newPoints)
    {
      points = newPoints;
      SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();

      width = rectTransform.rect.width;
      height = rectTransform.rect.height;

      if (points.Length < 2)
        return;

      float angle = 0;
      for (int i = 0; i < points.Length; i++)
      {
        Vector2 point = points[i];
        if (i < points.Length - 1)
          angle = GetAngle(points[i], points[i + 1]) + 45f;
        
        DrawVerticesForPoint(point, vh, angle);
      }
      
      for (int i = 0; i < points.Length-1; i++)
      {
        int index = i * 2;
        vh.AddTriangle(index, index + 1, index + 3 );
        vh.AddTriangle(index + 3, index + 2, index);
      }
      
    }

    private void DrawVerticesForPoint(Vector2 point, VertexHelper vertexHelper, float angle)
    {
      UIVertex vertex = UIVertex.simpleVert;

      vertex.color = color;
      vertex.position = VertexPosition(-thickness/2, point, angle);
      vertexHelper.AddVert(vertex);
      
      vertex.position = VertexPosition(thickness/2, point, angle);
      vertexHelper.AddVert(vertex);
    }

    private Vector3 VertexPosition(float startXPosition, Vector2 point, float angle)
    {
      Vector3 position;
      position = Quaternion.Euler(0,0,angle) * new Vector3(startXPosition, 0);
      position += new Vector3(point.x,  point.y);
      return position;
    }

    private float GetAngle(Vector2 at, Vector2 to)
    {
      return (float) (Mathf.Atan2(to.y - at.y, to.x - at.x) * (180 / Mathf.PI));
    }
  }
}