using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Player.Scripts.Steal
{
  public class HeroStealDisplayer : MonoBehaviour
  {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image prepareFill;
    
    private readonly CompositeDisposable disposable = new CompositeDisposable();
    private HeroStealPreparing stealPreparing;

    [Inject]
    public void Construct(HeroStealPreparing stealPreparing)
    {
      this.stealPreparing = stealPreparing;

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
      prepareFill.fillAmount = amount/stealPreparing.MaxValue;
  }
}