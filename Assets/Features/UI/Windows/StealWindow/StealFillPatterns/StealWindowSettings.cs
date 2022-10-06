using UnityEngine;

namespace Features.UI.Windows.StealWindow.StealFillPatterns
{
  [CreateAssetMenu(fileName = "StealWindowSettings", menuName = "StaticData/Steal/Create Steal Window Settings", order = 52)]
  public class StealWindowSettings : ScriptableObject
  {
    public int MinCoinsCount = 1;
    public StealWindowFillPattern[] FillPatterns;
    public LayerMask ItemsLayer;

    public StealWindowFillPattern RandomPattern() => 
      FillPatterns[Random.Range(0, FillPatterns.Length)];
  }
}