using System;
using Features.Player.Scripts.Steal;
using UniRx;
using UnityEngine;

namespace Features.Player.Scripts.Base
{
  public class HeroStealObserver : MonoBehaviour
  {
    [SerializeField] private HeroStealDisplayer displayer;
    
    private readonly CompositeDisposable disposable = new CompositeDisposable();
    
    public bool IsStealing { get; private set; }

    public void Construct(HeroStealPreparing stealPreparing)
    {
      stealPreparing.IsStealing.Subscribe(OnStealStateChange).AddTo(disposable);
      displayer.Construct(stealPreparing);
    }

    public void Cleanup() => 
      displayer.Cleanup();

    private void OnStealStateChange(bool isStealing)
    {
      IsStealing = isStealing;
      displayer.ChangeEnableState(isStealing);
    }
  }
}