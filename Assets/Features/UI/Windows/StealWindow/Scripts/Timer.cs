using System;
using UniRx;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class Timer
  {
    private readonly CompositeDisposable disposable;
    private bool isPaused;

    public readonly ReactiveCommand TimeOut;

    public readonly FloatReactiveProperty CurrentSeconds;
    public Timer()
    {
      disposable = new CompositeDisposable();
      TimeOut = new ReactiveCommand();
      CurrentSeconds = new FloatReactiveProperty(0);
    }

    public void Start(float seconds)
    {
      CurrentSeconds.Value = seconds;
      
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(onNext => Tick())
        .AddTo(disposable);
    }

    public void Stop() => 
      disposable.Clear();

    public void Pause() => 
      isPaused = true;

    public void Unpause() => 
      isPaused = false;

    private void Tick()
    {
      if (isPaused)
        return;
      
      CurrentSeconds.Value--;

      if (CurrentSeconds.Value <= 0)
        OnTimeOut();
    }

    private void OnTimeOut()
    {
      disposable.Clear();
      TimeOut.Execute();
    }
  }
}