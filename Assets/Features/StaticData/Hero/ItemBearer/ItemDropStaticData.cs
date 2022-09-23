using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.Hero.ItemBearer
{
  [CreateAssetMenu(fileName = "ItemDropStaticData", menuName = "StaticData/Hero/Item Drop Static Data", order = 52)]
  public class ItemDropStaticData : SerializedScriptableObject
  {
    [FoldoutGroup("Curve"),OnValueChanged("CalculateValues")]
    public AnimationCurve DropCurve;
    [FoldoutGroup("Curve"),ReadOnly]
    public float CurveRange;
    [FoldoutGroup("Curve"),ReadOnly]
    public float MaxCurveValue;
    [FoldoutGroup("Curve"),ReadOnly]
    public float CurveMinValue;
    [FoldoutGroup("Curve"),ReadOnly]
    public float CurveMaxTime;

    public LayerMask GroundMask;
    public float MaxDistance;
    
    
    private void CalculateValues()
    {
      if (DropCurve.keys.Length == 0)
        return;
      
      MaxCurveValue = DropCurve[0].value;
      CurveMinValue = DropCurve[DropCurve.length - 1].value;

      CurveRange = MaxCurveValue - CurveMinValue;

      CurveMaxTime = DropCurve[DropCurve.length - 1].time;
    }
  }
}