using Features.LevelArea;
using Features.LevelArea.Scripts;
using Features.Player.Scripts.Camera;
using Features.StaticData.LevelArea;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Features.Player.Scripts.AreaChange
{
  public class HeroAreaChangeObserver : MonoBehaviour
  {
    [SerializeField] private Collider2D checkCollider;
    
    private readonly CompositeDisposable disposable = new CompositeDisposable();
    private LevelBoundsObserver levelBoundsObserver;
    private HeroCamera heroCamera;

    private LevelAreaType currentArea;

    [Inject]
    public void Construct(LevelBoundsObserver levelBoundsObserver, HeroCamera heroCamera)
    {
      this.heroCamera = heroCamera;
      this.levelBoundsObserver = levelBoundsObserver;
      
      checkCollider.OnTriggerEnter2DAsObservable()
        .Where(other => other.TryGetComponent(out ChangeLevelAreaMarker marker))
        .Subscribe(other => ChangeArea(other.GetComponent<ChangeLevelAreaMarker>().Area))
        .AddTo(disposable);
    }

    public void SetStartArea(LevelAreaType area)
    {
      levelBoundsObserver.DisableMarkersIn(area);
      heroCamera.SetStartPosition(area);
      SetArea(area);
    }

    private void OnDestroy() => 
      disposable.Clear();

    private void ChangeArea(LevelAreaType area)
    {
      levelBoundsObserver.EnableMarkersIn(currentArea);
      levelBoundsObserver.DisableMarkersIn(area);
      heroCamera.MoveTo(area);

      SetArea(area);
    }

    private void SetArea(LevelAreaType area) => 
      currentArea = area;
  }
}