using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.Hero.AnimationTransitions
{
  [CreateAssetMenu(fileName = "BaseAnimationTransitionStaticData", menuName = "StaticData/Hero/Animation Transitions/Create Animation Transition", order = 52)]
  public class BaseAnimationTransitionStaticData : ScriptableObject
  {
    [OnValueChanged("UpdateTransitionsValue")]
    public string BaseParameterName;
    
    [ReadOnly]
    public int HashedName;
    
    private void UpdateTransitionsValue()
    {
      HashedName = Animator.StringToHash(BaseParameterName);
    }
  }
}