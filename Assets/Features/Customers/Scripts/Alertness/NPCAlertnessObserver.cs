using System;
using Features.Customers.Scripts.Timing;
using UnityEngine;

namespace Features.Customers.Scripts.Alertness
{
  public class NPCAlertnessObserver : MonoBehaviour
  {
    [SerializeField] private NPCTipsDisplayer tipsDisplayer;
    [SerializeField] private NPCAlertnessDisplayer displayer;
    
    private NPCAlertness alertness;
    private NPCExistTimeObserver timeObserver;
    public bool IsStealabale { get; private set; }
    public bool IsWary => alertness.IsWary.Value;

    public void Construct(NPCAlertness alertness, NPCExistTimeObserver timeObserver)
    {
      this.timeObserver = timeObserver;
      this.alertness = alertness;
      displayer.Construct(alertness);
    }

    public void AddAttention(int count) => 
      alertness.AddAttention(count);

    public void Warn() => 
      alertness.Warn();

    public void StartPayingAttention()
    {
      alertness.StartPayingAttention();
      timeObserver.Freeze();
    }

    public void StopPayingAttention()
    {
      alertness.StopPayingAttention();
      timeObserver.Unfreeze();
    }

    public void StartRelaxation() => 
      alertness.StartRelaxation();

    public void StopRelaxation() => 
      alertness.StopRelaxation();

    public void ShowWaryTip() => 
      tipsDisplayer.ShowWaryTip();

    public void HideWaryTip() => 
      tipsDisplayer.HideWaryTip();

    public void ShowAlertnessTip() => 
      tipsDisplayer.ShowAlertness();

    public void HideAlertnessTip() => 
      tipsDisplayer.HideAlertness();

    public void ShowStealTip() => 
      tipsDisplayer.ShowStealTip();

    public void HideStealTip() => 
      tipsDisplayer.HideStealTip();

    public void ChangeStealableState(bool isStealabale) => 
      IsStealabale = isStealabale;
  }
}