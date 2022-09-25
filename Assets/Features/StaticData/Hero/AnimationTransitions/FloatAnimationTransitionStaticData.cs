using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.Hero.AnimationTransitions
{
  [CreateAssetMenu(fileName = "HeroAnimationsTransitionStaticData", menuName = "StaticData/Hero/Animation Transitions/Create Float Animation Transitions", order = 52)]
  public class FloatAnimationTransitionStaticData : BaseAnimationTransitionStaticData
  {
    [OnValueChanged("ChangeFirstTransitionData")]
    public AnimationTransition FirstTransition;

    private void ChangeFirstTransitionData()
    {
      FirstTransition.HashedName = Animator.StringToHash(FirstTransition.ParameterName);
    }
  }
}