using System;
using Features.LevelArea.Scripts.ChangingArea;
using Features.Player.Scripts.Camera;
using Features.StaticData.LevelArea;

namespace Features.Player.Scripts.AreaChange
{
  public class HeroAreaChanger
  {
    private readonly LevelBoundsObserver levelBoundsObserver;
    private readonly HeroCamera heroCamera;

    private LevelAreaType currentArea;
    private LevelAreaType previousArea;

    public event Action<LevelAreaType, LevelAreaType> Changed; 

    public HeroAreaChanger(LevelBoundsObserver levelBoundsObserver, HeroCamera heroCamera, LevelAreaType startArea)
    {
      this.levelBoundsObserver = levelBoundsObserver;
      this.heroCamera = heroCamera;
      SetStartArea(startArea);
    }
    
    public void SetStartArea(LevelAreaType area)
    {
      levelBoundsObserver.DisableMarkersIn(area);
      heroCamera.SetStartPosition(area);
      SetStartValues(area);
    }
    
    public void ChangeArea(LevelAreaType area)
    {
      levelBoundsObserver.EnableMarkersIn(currentArea);
      levelBoundsObserver.DisableMarkersIn(area);
      heroCamera.MoveTo(area);

      SetArea(area);
    }
    
    private void SetArea(LevelAreaType area)
    {
      previousArea = currentArea;
      currentArea = area;
      NotifyAboutChange();
    }

    private void SetStartValues(LevelAreaType area)
    {
      previousArea = area;
      currentArea = area;
    }

    private void NotifyAboutChange()
    {
      Changed?.Invoke(previousArea, currentArea);
    }
  }
}