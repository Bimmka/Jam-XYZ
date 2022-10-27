using Features.UI.Windows.StealWindow.Scripts;
using UniRx;

namespace Features.Level.Scripts.LevelTimer
{
  public class LevelTimerObserver
  {
    private readonly CompositeDisposable disposable;

    private Timer timer;
    private int gameSeconds;

    public readonly ReactiveCommand<LevelTime> Changed;
    public readonly ReactiveCommand TimeOut;

    public LevelTimerObserver()
    {
      Changed = new ReactiveCommand<LevelTime>();
      TimeOut = new ReactiveCommand();
      disposable = new CompositeDisposable();
    }

    public void Start(int gameSeconds)
    {
      this.gameSeconds = gameSeconds;
      timer = new Timer();
      timer.CurrentSeconds.Subscribe(OnChangeTime).AddTo(disposable);
      timer.TimeOut.Subscribe(onNext => OnTimeOut()).AddTo(disposable);
      timer.Start(gameSeconds);
    }

    public void Stop()
    {
      timer.Stop();
      disposable.Clear();
    }

    public void Pause() => 
      timer.Pause();

    public void Unpause() => 
      timer.Unpause();

    private void OnChangeTime(float time) => 
      Changed.Execute(new LevelTime((int) time, gameSeconds));

    private void OnTimeOut()
    {
      timer.Stop();
      TimeOut.Execute();
    }
  }
}