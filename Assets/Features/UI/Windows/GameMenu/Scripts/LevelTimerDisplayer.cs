using Features.Level.Scripts.LevelTimer;
using TMPro;
using UniRx;
using UnityEngine;

namespace Features.UI.Windows.GameMenu.Scripts
{
  public class LevelTimerDisplayer : MonoBehaviour
  {
    [SerializeField] private string timeTextFormat;
    [SerializeField] private TextMeshProUGUI timeText;
        
    private LevelTimerObserver levelTimerObserver;
        
    private readonly CompositeDisposable disposable = new CompositeDisposable();

    public void Construct(LevelTimerObserver levelTimerObserver)
    {
      this.levelTimerObserver = levelTimerObserver;
      this.levelTimerObserver.Changed.Subscribe(Display).AddTo(disposable);
    }

    public void Cleanup()
    {
      disposable.Clear();
    }

    private void Display(LevelTime time) => 
      timeText.text = string.Format(timeTextFormat, time.Current);
  }
}