using System;

namespace Features.StaticData.Hero.AnimationTransitions
{
  [Serializable]
  public struct DoubleFloatTransition
  {
    public AnimationTransition FirstTransition;
    public AnimationTransition SecondTransition;
  }
}