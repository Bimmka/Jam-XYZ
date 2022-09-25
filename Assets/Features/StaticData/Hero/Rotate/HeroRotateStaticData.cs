using UnityEngine;

namespace Features.StaticData.Hero.Rotate
{
  [CreateAssetMenu(fileName = "HeroRotateStaticData", menuName = "StaticData/Hero/Create Hero Rotate Data", order = 52)]
  public class HeroRotateStaticData : ScriptableObject
  {
    [Range(0,1f)]
    public float WalkLerpRotateValue = 0.4f;
    
    [Range(0,1f)]
    public float RunLerpRotateValue = 0.5f;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
      WalkLerpRotateValue = Mathf.Clamp01(WalkLerpRotateValue);
      RunLerpRotateValue = Mathf.Clamp01(RunLerpRotateValue);
    }
#endif
  }
}