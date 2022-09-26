using UnityEngine;

namespace Features.StaticData.Hero.NPCSearching
{
  [CreateAssetMenu(fileName = "HeroNPCSearchingStaticData", menuName = "StaticData/Hero/Create NPC Searching Data", order = 52)]
  public class HeroNPCSearchingStaticData : ScriptableObject
  {
    public LayerMask NPCMask;
    public LayerMask ObstacleMask;
    public float Distance;
    public int MaxCount = 1;
    public float SearchDelay = 0.15f;
  }
}