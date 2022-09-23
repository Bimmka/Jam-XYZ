using UnityEngine;

namespace Features.StaticData.Hero.Interaction
{
  [CreateAssetMenu(fileName = "HeroInteractionStaticData", menuName = "StaticData/Hero/Hero Interaction Data", order = 52)]
  public class HeroInteractionStaticData : ScriptableObject
  {
    public int SearchInteractionCount = 5;
    public float SearchInterval = 0.15f;
    public LayerMask SearchMask;
    public LayerMask BlockingObjectsMask;
    public float Distance = 2f;
    public float Radius = 2f;
  }
}