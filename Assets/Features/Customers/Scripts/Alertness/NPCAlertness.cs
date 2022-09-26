using System;
using UniRx;

namespace Features.Customers.Scripts.Alertness
{
  public class NPCAlertness
  {
    private readonly float alertnessPerSecond;
    private readonly float relaxationPerSecond;
    private readonly float maxAlertness;
    
    private readonly CompositeDisposable disposable = new CompositeDisposable();
    
    public readonly FloatReactiveProperty CurrentAlertness = new FloatReactiveProperty(0);
    public readonly BoolReactiveProperty IsWary = new BoolReactiveProperty(false);

    public float MaxAlertness => maxAlertness;

    public NPCAlertness(float alertnessPerSecond, float relaxationPerSecond, float maxAlertness)
    {
      this.alertnessPerSecond = alertnessPerSecond;
      this.relaxationPerSecond = relaxationPerSecond;
      this.maxAlertness = maxAlertness;
    }

    public void StartPayingAttention()
    {
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(onNext => UpAlertness())
        .AddTo(disposable);
    }

    public void StopPayingAttention() => 
      disposable.Clear();

    public void StartRelaxation()
    {
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(onNext => DownAlertness())
        .AddTo(disposable);
    }

    public void StopRelaxation() => 
      disposable.Clear();

    private void UpAlertness()
    {
      CurrentAlertness.Value += alertnessPerSecond;

      if (IsAlertnessFilled())
        NotifyAboutFilled();
    }

    private void DownAlertness()
    {
      CurrentAlertness.Value -= relaxationPerSecond;

      if (IsAlertnessEmpty())
        StopRelaxation();
    }

    private bool IsAlertnessEmpty() => 
      CurrentAlertness.Value <= 0;

    private bool IsAlertnessFilled() => 
      CurrentAlertness.Value >= maxAlertness;

    private void NotifyAboutFilled() => 
      IsWary.Value = true;
  }
}