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

    public void Construct(NPCAlertness alertness)
    {
      alertness.CurrentAlertness.Subscribe(Display).AddTo(disposable);
    }

    public void Cleanup()
    {
      disposable.Clear();
    }

    public void ShowAlertness()
    {
      
    }

    public void HideAlertness()
    {
      
    }

    public void HideWaryTip()
    {
      
    }

    public void ShowWaryTip()
    {
      
    }

    private void Display(float value) => 
      alertnessSlider.DOFillAmount(value, fillDuration).SetEase(Ease.InOutSine);
  }
}