using TMPro;
using UniRx;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class TimerDisplayer : MonoBehaviour
  {
    [SerializeField] private string displayStringFormat;
    [SerializeField] private TextMeshProUGUI timeText;

    private readonly CompositeDisposable disposable = new CompositeDisposable();
    
    private Timer timer;

    public void Construct(Timer timer)
    {
      this.timer = timer;
      this.timer.CurrentSeconds.Subscribe(Display).AddTo(disposable);
    }

    public void Cleanup()
    {
      timer.Stop();
      disposable.Clear();
    }

    private void Display(float time) => 
      timeText.text = string.Format(displayStringFormat, timer.CurrentSeconds);
  }
}