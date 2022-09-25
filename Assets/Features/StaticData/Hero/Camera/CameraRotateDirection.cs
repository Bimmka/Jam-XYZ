using System;

namespace Features.StaticData.Hero.Camera
{
  [Serializable]
  public struct CameraRotateDirection
  {
    public DirectionType MainDirection;
    public DirectionType LeftDirection;
    public DirectionType RightDirection;
  }
}