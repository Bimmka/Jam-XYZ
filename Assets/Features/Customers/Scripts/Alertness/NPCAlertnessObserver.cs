using UnityEngine;

namespace Features.Customers.Scripts.Alertness
{
  public class NPCAlertnessObserver : MonoBehaviour
  {
    [SerializeField] private NPCAlertnessDisplayer displayer;
    
    private NPCAlertness alertness;

    public void Construct(NPCAlertness alertness)
    {
      this.alertness = alertness;
      displayer.Construct(alertness);
    }

    public void StartPayingAttention() => 
      alertness.StartPayingAttention();

    public void StopPayingAttention() => 
      alertness.StopPayingAttention();

    public void StartRelaxation() => 
      alertness.StartRelaxation();

    public void StopRelaxation() => 
      alertness.StopRelaxation();

    public void ShowWaryTip() => 
      displayer.ShowWaryTip();

    public void HideWaryTip() => 
      displayer.HideWaryTip();
    
    public void ShowAlertnessTip() => 
      displayer.ShowAlertness();

    public void HideAlertnessTip() => 
      displayer.HideAlertness();
  }
}