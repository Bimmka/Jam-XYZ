using UnityEngine;

namespace Features.StaticData.Hero.Stealing
{
  [CreateAssetMenu(fileName = "HeroStealingStaticData", menuName = "StaticData/Hero/Stealing/Create Stealing Data", order = 52)]
  public class HeroStealingStaticData : ScriptableObject
  {
    public float PreparePerSecond = 1f;
    public float MaxPrepareCount = 10f;
  }
}