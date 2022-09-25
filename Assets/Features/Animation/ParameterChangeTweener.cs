using System;
using DG.Tweening;
using UnityEngine;

namespace Features.Animation
{
  public class ParameterChangeTweener
  {
    private readonly int hashName;
    private readonly Animator animator;
    
    private Tweener tweener;
    
    public float EndValue { get; private set; }
    public event Action<int> Ended;

    public ParameterChangeTweener(int hashName, Animator animator)
    {
      this.hashName = hashName;
      this.animator = animator;
    }

    public void StopProcess() => 
      tweener.Kill();

    public void StartProcess(float endValue, float duration)
    {
      EndValue = endValue;
      tweener = DOTween.To(ParameterValue, SetParameterValue, endValue, duration).OnComplete(OnTweenEnd);
    }

    private void OnTweenEnd() => 
      Ended?.Invoke(hashName);

    private float ParameterValue() => 
      animator.GetFloat(hashName);

    private void SetParameterValue(float value) => 
      animator.SetFloat(hashName, value);
  }
}