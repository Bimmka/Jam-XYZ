using System;
using UniRx;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class Timer
  {
    private readonly CompositeDisposable disposable;
    private bool isPaused;

    public readonly ReactiveCommand TimeOut;
    
    public float CurrentSeconds { get; private set; }

    public event Action<float> Changed; 
    public Timer()
    {
      disposable = new CompositeDisposable();
      TimeOut = new ReactiveCommand();
    }

    public void Start(float seconds)
    {
      CurrentSeconds = seconds;
      
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
      
      CurrentSeconds--;

      NotifyAboutChange();
      
      if (CurrentSeconds <= 0)
        OnTimeOut();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke(CurrentSeconds);

    private void OnTimeOut()
    {
      disposable.Clear();
      TimeOut.Execute();
    }
  }
}