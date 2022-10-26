using TMPro;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class TimerDisplayer : MonoBehaviour
  {
    [SerializeField] private string displayStringFormat;
    [SerializeField] private TextMeshProUGUI timeText;

    private Timer timer;

    public void Construct(Timer timer)
    {
      this.timer = timer;
      this.timer.Changed += Display;
    }

    public void Cleanup()
    {
      timer.Stop();
      timer.Changed -= Display;
    }

    private void Display(float time) => 
      timeText.text = string.Format(displayStringFormat, timer.CurrentSeconds);
  }
}