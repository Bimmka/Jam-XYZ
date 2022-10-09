using Features.Tweeners;
using UnityEngine;
using UnityEngine.UI;

namespace Features.StealItems.Scripts
{
  public class StealItemView : MonoBehaviour
  {
    [SerializeField] private Image view;
    [SerializeField] private RandomRotateTweener rotateTweener;

    public void SetView(Sprite sprite)
    {
      view.sprite = sprite;
    }

    public void StartAnimation() => 
      rotateTweener.StartRotate();

    public void StopAnimation() => 
      rotateTweener.StopRotate();
  }
}