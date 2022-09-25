using UnityEngine;

namespace Features.StaticData.Hero.Rotate
{
  [CreateAssetMenu(fileName = "HeroRotateStaticData", menuName = "StaticData/Hero/Create Hero Rotate Data", order = 52)]
  public class HeroRotateStaticData : ScriptableObject
  {

    [Range(0,0.1f)]
    public float RunLerpRotateValue = 0.05f;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
      RunLerpRotateValue = Mathf.Clamp01(RunLerpRotateValue);
    }
#endif
  }
}