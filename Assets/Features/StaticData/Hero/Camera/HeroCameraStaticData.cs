using UnityEngine;

namespace Features.StaticData.Hero.Camera
{
  [CreateAssetMenu(fileName = "HeroCameraStaticData", menuName = "StaticData/Hero/Create Camera Data", order = 52)]
  public class HeroCameraStaticData : ScriptableObject
  {
    public DirectionType StartDirection;
    public CameraPosition[] Positions;
    public CameraRotateDirection[] Directions;
    public float RotateDuration = 1f;
    

    public Vector3 Position(DirectionType directionType)
    {
      for (int i = 0; i < Positions.Length; i++)
      {
        if (Positions[i].Direction == directionType)
          return Positions[i].Position;
      }
      
      return Vector3.zero;
    }

    public DirectionType NextDirection(DirectionType mainDirection, float rotateDirection)
    {
      if (rotateDirection == 0)
        return mainDirection;
      
      for (int i = 0; i < Directions.Length; i++)
      {
        if (Directions[i].MainDirection == mainDirection)
          return rotateDirection > 0 ? Directions[i].RightDirection : Directions[i].LeftDirection;
      }

      return mainDirection;
    }
  }
}