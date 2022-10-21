using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Tweeners
{
  public class RandomRotateTweener : MonoBehaviour
  {
    [SerializeField] private Vector2 rotateStepRange;
    [SerializeField] private Vector2 rotateDurationRange;
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private Transform target;
    [SerializeField] private Ease ease;
    [SerializeField] private bool isLoop;

    private Tweener tweener;

    private float currentRotateStep;
    private float currentRotateDuration;

    private void Awake()
    {
      currentRotateStep = Random.Range(rotateStepRange.x, rotateStepRange.y);
      currentRotateDuration = Random.Range(rotateDurationRange.x, rotateDurationRange.y);
    }

    private void OnDestroy()
    {
      
    }

    public void StartRotate()
    {
      if (isLoop)
        StartLoopRotate();
      else
        StartOnceRotate();
    }

    private void StartLoopRotate() =>
      tweener = target
        .DORotate(target.transform.eulerAngles + rotateAxis * currentRotateStep, currentRotateDuration)
        .SetEase(ease)
        .OnComplete(StartLoopRotate);

    private void StartOnceRotate()
    {
      tweener = target
        .DORotate(target.transform.eulerAngles + rotateAxis * currentRotateStep, currentRotateDuration)
        .SetEase(ease);
    }

    public void StopRotate()
    {
      if (tweener != null && tweener.IsPlaying())
        tweener.Kill();
    }
  }
}