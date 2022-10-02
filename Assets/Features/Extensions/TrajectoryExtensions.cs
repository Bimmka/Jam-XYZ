using UnityEngine;

namespace Features.Extensions
{
  public class TrajectoryExtensions
  {
    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float time)
    {
      Vector2 middlePoint = Vector2.Lerp(start, end, time);
      float position = -4 * height * time * time + 4 * height * time;
      
      return new Vector2(middlePoint.x, position + Mathf.Lerp(start.y, end.y, time));
    }
  }
}