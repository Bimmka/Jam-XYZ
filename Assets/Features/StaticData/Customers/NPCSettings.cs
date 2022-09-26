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
    public float AlertnessPerSecond = 0.1f;
    public float RelaxationPerSecond = 0.05f;
    public float MaxAlertness = 100;
    public float Speed = 3f;
  }
}