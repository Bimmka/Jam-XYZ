using System.Collections.Generic;
using UnityEngine;

namespace Features.Animation
{
  public class AnimatorParametersChanger
  {
    private readonly Animator animator;
    private readonly Dictionary<int, ParameterChangeTweener> changeProcesses;
    
    public AnimatorParametersChanger(Animator animator, AnimatorControllerParameter[] parameters)
    {
      this.animator = animator;

      changeProcesses = new Dictionary<int, ParameterChangeTweener>(parameters.Length);

      for (int i = 0; i < parameters.Length; i++)
      {
        if (parameters[i].type == AnimatorControllerParameterType.Float)
          changeProcesses.Add(parameters[i].nameHash, null);
      }
    }

    public void ChangeParameter(int hashName, float endValue, float duration)
    {
      if (changeProcesses.ContainsKey(hashName) == false)
        return;
      
      if (IsSameChangeTweener(hashName, endValue))
        return;

      if (IsHaveTweener(hashName))
        RestartProcess(hashName, endValue, duration, animator);
      else
        StartProcess(hashName, endValue, duration, animator);
    }

    private void RestartProcess(in int hashName, in float endValue, in float duration, Animator animator)
    {
      changeProcesses[hashName].StopProcess();
      changeProcesses[hashName].Ended -= OnTweenerEnd;
      StartProcess(hashName, endValue, duration, animator);
    }

    private void StartProcess(in int hashName, in float endValue, in float duration, Animator animator)
    {
      ParameterChangeTweener tweener = new ParameterChangeTweener(hashName, animator);
      changeProcesses[hashName] = tweener;
      tweener.Ended += OnTweenerEnd;
      
      changeProcesses[hashName].StartProcess(endValue, duration);
    }

    private void OnTweenerEnd(int hashName)
    {
      changeProcesses[hashName].Ended -= OnTweenerEnd;
      changeProcesses[hashName] = null;
    }

    private bool IsSameChangeTweener(int hashName, float endValue) => 
      IsHaveTweener(hashName) && Mathf.Approximately(changeProcesses[hashName].EndValue, endValue);

    private bool IsHaveTweener(in int hashName) => 
      changeProcesses[hashName] != null;
  }
}