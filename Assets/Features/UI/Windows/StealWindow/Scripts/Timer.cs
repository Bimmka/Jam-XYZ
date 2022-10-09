using System;
using UniRx;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class Timer
  {
    private readonly CompositeDisposable disposable;

    public readonly ReactiveCommand TimeOut;
    
    public float CurrentSeconds { get; private set; }
    public Timer(float seconds)
    {
      CurrentSeconds = seconds;
      
      disposable = new CompositeDisposable();
      TimeOut = new ReactiveCommand();
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(onNext => Tick())
        .AddTo(disposable);
    }

    private void Tick()
    {
      CurrentSeconds--;
      
      if (CurrentSeconds <= 0)
        OnTimeOut();
    }

    private void OnTimeOut()
    {
      disposable.Clear();
      TimeOut.Execute();
    }
  }
}