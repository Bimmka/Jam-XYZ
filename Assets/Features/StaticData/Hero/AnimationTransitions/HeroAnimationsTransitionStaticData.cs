using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.StaticData.Hero.AnimationTransitions
{
  [CreateAssetMenu(fileName = "HeroAnimationsTransitionStaticData", menuName = "StaticData/Hero/Animation Transitions/Create Animation Transitions", order = 52)]
  public class HeroAnimationsTransitionStaticData : SerializedScriptableObject
  {
    [DictionaryDrawerSettings(KeyLabel = "State Name", ValueLabel = "Transition Data")]
    public Dictionary<string, BaseAnimationTransitionStaticData> Transitions;
  }
}