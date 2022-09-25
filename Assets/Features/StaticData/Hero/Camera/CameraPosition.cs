using System;
using UnityEngine;

namespace Features.StaticData.Hero.Camera
{
  [Serializable]
  public struct CameraPosition
  {
    public DirectionType Direction;
    public Vector3 Position;
  }
}