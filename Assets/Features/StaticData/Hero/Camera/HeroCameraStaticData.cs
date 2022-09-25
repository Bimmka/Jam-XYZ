using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.StaticData.Hero.Camera
{
  [CreateAssetMenu(fileName = "HeroCameraStaticData", menuName = "StaticData/Hero/Create Camera Data", order = 52)]
  public class HeroCameraStaticData : ScriptableObject
  {
    public CameraAreaData[] Positions;
    public LevelAreaType StartArea;
    public float MoveDuration = 1f;


    public Vector3 Position(LevelAreaType area)
    {
      for (int i = 0; i < Positions.Length; i++)
      {
        if (Positions[i].Area == area)
          return Positions[i].Position;
      }
      
      return Vector3.zero;
    }
  }
}