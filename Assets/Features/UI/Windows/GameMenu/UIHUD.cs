using System;
using Features.Player.Scripts.Gold;
using Features.UI.Windows.Base;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Windows.GameMenu
{
    public class UIHUD : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        [Inject]
        public void Construct(HeroGold gold)
        {
            gold.Count.Subscribe(Display).AddTo(disposable);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            disposable.Clear();
        }

        private void Display(int count) => 
            scoreDisplay.text = count.ToString();
    }
}
