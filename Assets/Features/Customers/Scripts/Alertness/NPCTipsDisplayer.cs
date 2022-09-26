using UnityEngine;
using UnityEngine.UI;

namespace Features.Customers.Scripts.Alertness
{
  public class NPCTipsDisplayer : MonoBehaviour
  {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject alertTip;
    [SerializeField] private GameObject stealTip;
    [SerializeField] private GameObject waryTip;
    [SerializeField] private GameObject alertPercentTip;

    public void ShowAlertness()
    {
      ActivateCanvas();
      ChangeTipActiveState(alertPercentTip, true);
      ChangeTipActiveState(alertTip, true);
    }

    public void HideAlertness()
    {
      DisableCanvas();
      ChangeTipActiveState(alertPercentTip, false);
      ChangeTipActiveState(alertTip, false);
    }

    public void ShowWaryTip()
    {
      ActivateCanvas();
      ChangeTipActiveState(waryTip, true);
    }

    public void HideWaryTip()
    {
      DisableCanvas();
      ChangeTipActiveState(waryTip, false);
    }

    public void ShowStealTip()
    {
      ActivateCanvas();
      ChangeTipActiveState(alertPercentTip, true);
      ChangeTipActiveState(stealTip, true);
    }

    public void HideStealTip()
    {
      DisableCanvas();
      ChangeTipActiveState(alertPercentTip, false);
      ChangeTipActiveState(stealTip, false);
    }

    private void ActivateCanvas() => 
      canvasGroup.alpha = 1;

    private void DisableCanvas() => 
      canvasGroup.alpha = 0;

    private void ChangeTipActiveState(GameObject tip, bool isActive)
    {
      if (tip.activeSelf == isActive)
        return;
      
      tip.SetActive(isActive);
    }
  }
}