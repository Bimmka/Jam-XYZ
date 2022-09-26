using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Player.Scripts.Steal
{
  public class HeroStealDisplayer : MonoBehaviour
  {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image prepareFill;
    
    private CompositeDisposable disposable = new CompositeDisposable();
    private float maxStealCount;

    public void Construct(HeroStealPreparing stealPreparing, float maxStealCount)
    {
      this.maxStealCount = maxStealCount;
    
      stealPreparing.IsStealing.Subscribe(OnStealStateChange).AddTo(disposable);
      stealPreparing.PrepareAmount.Subscribe(DisplayPrepare).AddTo(disposable);
    }

    public void Cleanup()
    {
      disposable.Clear();
    }

    private void OnStealStateChange(bool isStealing) => 
      canvasGroup.alpha = isStealing ? 1 : 0;

    private void DisplayPrepare(float amount) => 
      prepareFill.fillAmount = amount/maxStealCount;
  }
}