using System;
using UniRx;

namespace Features.Customers.Scripts.Timing
{
  public class NPCExistTimeObserver
  {
    private float existSeconds;
    private bool isFreeze;
    
    private CompositeDisposable disposable = new CompositeDisposable();

    public readonly BoolReactiveProperty IsNeedToExit = new BoolReactiveProperty(false);

    public NPCExistTimeObserver(float existSeconds)
    {
      this.existSeconds = existSeconds;
      StartTimer();
    }

    public void Freeze() => 
      isFreeze = true;

    public void Unfreeze() => 
      isFreeze = false;

    public void StartTimer()
    {
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(onNext => UpdateTimer())
        .AddTo(disposable);
    }

    private void UpdateTimer()
    {
      if (isFreeze)
        return;
      
      existSeconds --;
      
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