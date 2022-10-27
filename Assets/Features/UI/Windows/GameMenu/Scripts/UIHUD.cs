using Features.Level.Scripts.LevelTimer;
using Features.Player.Scripts.Gold;
using Features.UI.Windows.Base;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Windows.GameMenu.Scripts
{
    public class UIHUD : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private LevelTimerDisplayer timerDisplayer;
        
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        [Inject]
        public void Construct(HeroGold gold, LevelTimerObserver levelTimerObserver)
        {
            gold.Count.Subscribe(Display).AddTo(disposable);
            timerDisplayer.Construct(levelTimerObserver);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            disposable.Clear();
            timerDisplayer.Cleanup();
        }

        private void Display(int count) => 
            scoreDisplay.text = count.ToString();
    }
}
