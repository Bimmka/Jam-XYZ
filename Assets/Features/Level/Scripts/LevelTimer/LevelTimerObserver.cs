using System;
using Features.UI.Windows.StealWindow.Scripts;

namespace Features.Level.Scripts.LevelTimer
{
  public class LevelTimerObserver
  {
    private int gameSeconds;
    
    private Timer timer;

    public event Action<int, int> Changed;

    public void Start(int gameSeconds)
    {
      this.gameSeconds = gameSeconds;
      timer = new Timer();
      timer.Changed += OnChangeTime;
      timer.Start(gameSeconds);
    }

    public void Stop()
    {
      timer.Stop();
      timer.Changed -= OnChangeTime;
    }

    public void Pause() => 
      timer.Pause();

    public void Unpause() => 
      timer.Unpause();

    private void OnChangeTime(float time)
    {
      Changed?.Invoke((int) time, gameSeconds);
    }
  }
}