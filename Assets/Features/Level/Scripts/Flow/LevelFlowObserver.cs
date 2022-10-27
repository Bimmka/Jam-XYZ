using Features.Customers.Scripts.Factory;
using Features.Level.Scripts.LevelTimer;
using Features.Player.Scripts.Base;
using Features.Police.Scripts.Observer;
using Features.StaticData.LevelArea;
using UniRx;

namespace Features.Level.Scripts.Flow
{
  public class LevelFlowObserver
  {
    private readonly LevelTimerObserver levelTimerObserver;
    private readonly NPCObserver npcObserver;
    private readonly PoliceObserver policeObserver;
    private readonly LevelStaticData levelStaticData;
    private readonly Hero hero;
    private readonly CompositeDisposable disposable;

    public LevelFlowObserver(LevelTimerObserver levelTimerObserver, NPCObserver npcObserver, PoliceObserver policeObserver,
      LevelStaticData levelStaticData, Hero hero)
    {
      disposable = new CompositeDisposable();
      this.levelTimerObserver = levelTimerObserver;
      this.levelTimerObserver.TimeOut.Subscribe(onNext => FinishGame()).AddTo(disposable);
      this.npcObserver = npcObserver;
      this.policeObserver = policeObserver;
      this.levelStaticData = levelStaticData;
      this.hero = hero;
    }

    public void Cleanup()
    {
      levelTimerObserver.Stop();
      npcObserver.Cleanup();
      policeObserver.Cleanup();
      disposable.Clear();
    }

    public void StartLevel()
    {
      levelTimerObserver.Start(levelStaticData.SecondsForGame);
      hero.Enable();
    }

    private void FinishGame()
    {
      
    }
  }
}