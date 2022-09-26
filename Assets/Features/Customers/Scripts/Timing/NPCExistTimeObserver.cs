using System;
using UniRx;

namespace Features.Customers.Scripts.Timing
{
  public class NPCExistTimeObserver
  {
    private float existSeconds;
    
    private CompositeDisposable disposable = new CompositeDisposable();

    public BoolReactiveProperty IsNeedToExit = new BoolReactiveProperty(false);

    public NPCExistTimeObserver(float existSeconds)
    {
      this.existSeconds = existSeconds;
      StartTimer();
    }

    public void StartTimer()
    {
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(UpdateTimer)
        .AddTo(disposable);
    }

    private void UpdateTimer(long source)
    {
      existSeconds -= source;
      
      if (existSeconds <= 0)
        OnTimerFinished();
    }

    private void OnTimerFinished()
    {
      IsNeedToExit.Value = true;
      disposable.Clear();
    }
  }
}