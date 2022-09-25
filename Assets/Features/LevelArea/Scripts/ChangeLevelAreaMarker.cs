using Features.StaticData.LevelArea;
using UnityEngine;

namespace Features.LevelArea.Scripts
{
  public class ChangeLevelAreaMarker : MonoBehaviour
  {
    [SerializeField] private Collider2D markerCollider;
    
    public LevelAreaType Area { get; private set; }

    public void Initialize(LevelAreaType area)
    {
      Area = area;
    }

    public void Disable() => 
      markerCollider.enabled = false;

    public void Enable() => 
      markerCollider.enabled = true;
  }
}