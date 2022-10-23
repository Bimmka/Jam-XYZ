using DG.Tweening;
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
    [SerializeField] private float fillDuration = 1f;
    
    private readonly CompositeDisposable disposable = new CompositeDisposable();
    private HeroStealPreparing stealPreparing;

    [Inject]
    public void Construct(HeroStealPreparing stealPreparing)
    {
      this.stealPreparing = stealPreparing;

      stealPreparing.IsStealing.Subscribe(ChangeEnableState).AddTo(disposable);
      stealPreparing.PrepareAmount.Subscribe(DisplayPrepare).AddTo(disposable);
    }

    public void Cleanup()
    {
      disposable.Clear();
    }

    public void ChangeEnableState(bool isStealing) => 
      canvasGroup.alpha = isStealing ? 1 : 0;

    private void DisplayPrepare(float amount) => 
      prepareFill.DOFillAmount(amount / stealPreparing.MaxValue, fillDuration).SetEase(Ease.InOutSine);
  }
}