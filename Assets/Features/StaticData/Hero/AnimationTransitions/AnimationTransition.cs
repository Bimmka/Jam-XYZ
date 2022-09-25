using System;
using Sirenix.OdinInspector;

namespace Features.StaticData.Hero.AnimationTransitions
{
  [Serializable]
  public struct AnimationTransition
  {
    public string ParameterName;
    [ReadOnly]
    public int HashedName;
    public float EndValue;
    public float Duration;
  }
}