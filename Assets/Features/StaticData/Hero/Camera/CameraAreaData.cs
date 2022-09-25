using System;
using Features.StaticData.LevelArea;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.Hero.Camera
{
  [Serializable]
  public struct CameraAreaData
  {
    public LevelAreaType Area;
    [ReadOnly]
    public Vector3 Position;
    [OnValueChanged("UpdatePosition")]
    public Transform FollowedObject;

    private void UpdatePosition()
    {
      if (FollowedObject != null)
        Position = FollowedObject.position;
    }
    
  }
}