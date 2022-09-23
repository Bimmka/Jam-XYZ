using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.Hero.AnimationTransitions
{
  [CreateAssetMenu(fileName = "HeroAnimationsTransitionStaticData", menuName = "StaticData/Hero/Animation Transitions/Create Double Float Animation Transitions", order = 52)]
  public class DoubleFloatAnimationTransitionStaticData : FloatAnimationTransitionStaticData
  {
    [OnValueChanged("ChangeSecondTransitionData")]
    public AnimationTransition SecondTransition;

    private void ChangeSecondTransitionData()
    {
      SecondTransition.HashedName = Animator.StringToHash(SecondTransition.ParameterName);
    }
  }
}