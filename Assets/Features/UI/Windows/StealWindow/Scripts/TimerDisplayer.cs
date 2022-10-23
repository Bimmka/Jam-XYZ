using System.Collections;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.StealWindow.Scripts
{
  public class TimerDisplayer : MonoBehaviour
  {
    [SerializeField] private string displayStringFormat;
    [SerializeField] private TextMeshProUGUI timeText;

    private bool isDisplaying;
    
    private Timer timer;

    public void Construct(Timer timer)
    {
      this.timer = timer;
    }

    public void StartObserve()
    {
      isDisplaying = true;
      StartCoroutine(Display());
    }

    public void StopObserve()
    {
      isDisplaying = false;
      StopAllCoroutines();
    }

    private IEnumerator Display()
    {
      while (isDisplaying)
      {
        timeText.text = string.Format(displayStringFormat, timer.CurrentSeconds);
        yield return null;
      }
    }
  }
}