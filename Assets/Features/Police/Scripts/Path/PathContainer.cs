using UnityEngine;

namespace Features.Police.Scripts.Path
{
  public class PathContainer
  {
    private readonly Vector3[] path;

    private byte currentIndex;
    private short direction;

    public PathContainer(Vector3[] path)
    {
      this.path = path;
      direction = 1;
    }

    public void RecalculateIndex(Vector3 policePosition)
    {
      float calculatedDistance;
      float minDistance = float.MaxValue;
      byte index = 0;
      Vector3 differenceVector;

      for (byte i = 0; i < path.Length; i++)
      {
        differenceVector = path[i] - policePosition;
        calculatedDistance = Vector3.Magnitude(differenceVector);

        if (calculatedDistance < minDistance)
        {
          minDistance = calculatedDistance;
          index = i;
        }
      }

      currentIndex = index;
    }

    public void IncIndex()
    {
      if (currentIndex + direction >= path.Length) 
        direction = -1;
      else if (currentIndex + direction < 0)
        direction = 1;

      currentIndex = (byte) (currentIndex + direction);
    }

    public Vector3 Position() => 
      path[currentIndex];
  }
}