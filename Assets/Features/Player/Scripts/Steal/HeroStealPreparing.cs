using System;
using Features.StaticData.Hero.Stealing;
using UniRx;

namespace Features.Player.Scripts.Steal
{
  public class HeroStealPreparing
  {
    public readonly FloatReactiveProperty PrepareAmount = new FloatReactiveProperty(0);
    public readonly BoolReactiveProperty IsStealing = new BoolReactiveProperty();
    public readonly BoolReactiveProperty IsFullPrepared = new BoolReactiveProperty();
    
    private readonly HeroStealingStaticData stealingData;
    private readonly CompositeDisposable disposable = new CompositeDisposable();

    public float MaxValue => stealingData.MaxPrepareCount;
    
    public HeroStealPreparing(HeroStealingStaticData stealingData)
    {
      this.stealingData = stealingData;
    }

    public void StartStealPrepare()
    {
      Observable
        .Interval(TimeSpan.FromSeconds(1))
        .Subscribe(onNext => UpPrepare())
        .AddTo(disposable);

      IsStealing.Value = true;
    }

    public void ResetPreparing()
    {
      PrepareAmount.Value = 0;
      IsStealing.Value = false;
      IsFullPrepared.Value = false;
      
      disposable.Clear();
    }

    private void UpPrepare()
    {
      PrepareAmount.Value += stealingData.PreparePerSecond;

      if (IsFullPrepare())
        SetFullPrepared();
    }

    private void SetFullPrepared() => 
      IsFullPrepared.Value = true;

    private bool IsFullPrepare() => 
      PrepareAmount.Value >= stealingData.MaxPrepareCount;
  }
}