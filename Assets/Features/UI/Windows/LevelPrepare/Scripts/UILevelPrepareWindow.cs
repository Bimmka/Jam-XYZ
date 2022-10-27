using Features.Level.Scripts.Flow;
using Features.Level.Scripts.Goal;
using Features.UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.LevelPrepare.Scripts
{
  public class UILevelPrepareWindow : BaseWindow
  {
    [SerializeField] private Button playButton;
    
    private LevelGoal levelGoal;
    private LevelFlowObserver levelFlowObserver;

    [Inject]
    public void Construct(LevelGoal levelGoal, LevelFlowObserver levelFlowObserver)
    {
      this.levelFlowObserver = levelFlowObserver;
      this.levelGoal = levelGoal;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      playButton.onClick.AddListener(StartLevel);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      playButton.onClick.RemoveListener(StartLevel);
    }

    private void StartLevel()
    {
      levelFlowObserver.StartLevel();
      Destroy();
    }
  }
}