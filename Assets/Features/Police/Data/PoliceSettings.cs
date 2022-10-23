using Features.Police.Scripts.PoliceStates;
using UnityEngine;

namespace Features.Police.Data
{
  [CreateAssetMenu(fileName = "PoliceSettings", menuName = "StaticData/Police/Create Police Settings", order = 52)]
  public class PoliceSettings : ScriptableObject
  {
    public PoliceType Type;
    public float PatrolSpeed;
    public float FollowSpeed;
    public float SearchSpeed;
    public float WarnedSpeed;
    public float AngleOfView;
    public Vector2 PlayerRaycastBoxSize;
    public float ObstacleRaycastSphereRadius = 0.3f;
    public float RaycastDistance = 1f;
    public LayerMask PlayerMask;
    public LayerMask ObstacleMask;
    public int PlayerMaxHitCount = 1;
    public int ObstacleMaxHitCount = 3;
    public float SearchDelay;
    public PoliceOfficer Prefab;
  }
}