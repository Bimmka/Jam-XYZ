using Features.Customers.Scripts.Base;
using UnityEngine;

namespace Features.StaticData.Customers
{
  [CreateAssetMenu(fileName = "NPCSettings", menuName = "StaticData/NPC/Create NPC", order = 52)]
  public class NPCSettings : ScriptableObject
  {
    public NPCType Type;
    public NPC View;
    public Vector2 ExistSecondsRange;
    public float AlertnessPerFrame = 0.1f;
    public float RelaxationPerFrame = 0.05f;
    public float MaxAlertness = 100;
  }
}