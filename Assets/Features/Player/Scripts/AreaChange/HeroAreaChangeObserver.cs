using Features.Level.Scripts.ChangingArea;
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
    private HeroAreaChanger areaChanger;


    [Inject]
    public void Construct(HeroAreaChanger areaChanger)
    {
      this.areaChanger = areaChanger;

      checkCollider.OnTriggerEnter2DAsObservable()
        .Where(other => other.TryGetComponent(out ChangeLevelAreaMarker marker))
        .Subscribe(other => ChangeArea(other.GetComponent<ChangeLevelAreaMarker>().Area))
        .AddTo(disposable);
    }

    private void OnDestroy() => 
      disposable.Clear();

    public void SetStartArea(LevelAreaType area) => 
      areaChanger.SetStartArea(area);

    private void ChangeArea(LevelAreaType area) => 
      areaChanger.ChangeArea(area);
  }
}