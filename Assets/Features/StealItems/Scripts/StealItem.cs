using Features.UI.Windows.StealWindow.Scripts;
using UnityEngine;

namespace Features.StealItems.Scripts
{
  [RequireComponent(typeof(StealItemView))]
  public class StealItem : MonoBehaviour
  {
    [SerializeField] private StealItemView view;
    public StealItemType Type { get; private set; }

    public void Initialize(StealItemType type, Sprite view)
    {
      Type = type;
      this.view.SetView(view);
    }

    private void Start()
    {
      view.StartAnimation();
    }

    private void OnDestroy()
    {
      Hide();
    }

    public void Hide()
    {
      view.StopAnimation();
      gameObject.SetActive(false);
    }
  }
}