using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Customers.Scripts.Alertness
{
  public class NPCAlertnessDisplayer : MonoBehaviour
  {
    [SerializeField] private Image alertnessSlider;
    [SerializeField] private float fillDuration;
    
    private CompositeDisposable disposable = new CompositeDisposable();
    private NPCAlertness alertness;

    public void Construct(NPCAlertness alertness)
    {
      this.alertness = alertness;
      alertness.CurrentAlertness.Subscribe(Display).AddTo(disposable);
    }

    public void Cleanup()
    {
      disposable.Clear();
    }

    

    private void Display(float value) => 
      alertnessSlider.DOFillAmount(value / alertness.MaxAlertness, fillDuration).SetEase(Ease.InOutSine);
  }
}